using A9DbConn01;
using A9Library;
using A9Library.DATABASE;
using DbTransferWF.Model;
using LibDbTransf;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace DbTransferWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            CarregarDadosConBD();

            if (Debugger.IsAttached)
                txtTab.Text = "tb_cep01";

        }

        private void btnConfCon_Click(object sender, EventArgs e)
        {
            View_ConfCon w = new View_ConfCon();

            this.Visible = false;

            w.ShowDialog();

            this.Visible = true;

            CarregarDadosConBD();
        }

        private void CarregarDadosConBD()
        {
            bsBDDes.DataSource = BancoDados01.ObterDadosFromJson();
            bsBDOri.DataSource = BancoDados01.ObterDadosFromJson();
        }

        private void UpdateLog(string text, bool isError = false)
        {
            rchtxtlog.ForeColor = isError ? Color.Red : Color.Black;
            rchtxtlog.AppendText(string.Format("{0}\n", text));
            rchtxtlog.ScrollToCaret();
            rchtxtlog.Refresh();
        }

        private void btnIni_Click(object sender, EventArgs e)
        {
            try
            {
                rchtxtlog.Clear();
                progressBar1.Value = 0;

                DateTime timeIni;
                DateTime timeFim;

                DataTable result;

                var dbOrigem = cmbBDOri.SelectedItem as BancoDados01;
                var dbDestino = cmbBDDes.SelectedItem as BancoDados01;

                bool isInsert = ckbInsert.Checked;
                bool isUpdate = ckbUpdate.Checked;

                string tabela = txtTab.Text;
                string filtro = txtFil.Text;
                string schema = txtSchDes.Text;

                int regcommit = Convert.ToInt32(numCom.Value);
                int totalIns = 0;
                int totalUpd = 0;

                if (isInsert == false && isUpdate == false)
                    throw new Exception("Para realizar esta operação marque Insert ou Update!");

                if (dbOrigem?.StringConexao == dbDestino?.StringConexao)
                    throw new Exception("Banco de dados de Origem e Destino não devem possuir a mesma string de conexão!");

                if (dbDestino.IsProducao)
                {
                    var dr = MessageBox.Show("Você está preste a realizar uma atualização de registros em um banco de dados de PRODUÇÃO\n" +
                        "Você deseja sequir com esta operação?", "Atenção!", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.No)
                        return;

                    var senha = Microsoft.VisualBasic.Interaction.InputBox(String.Format("Informe a senha do usuário {0} de {1}.", dbDestino.User, dbDestino.Descricao), "Validação", "*", 500, 150);

                    if (string.IsNullOrEmpty(senha)) return;

                    if (senha != dbDestino.Password)
                    {
                        MessageBox.Show("Senha incorreta");
                        return;
                    }
                }

                if (string.IsNullOrEmpty(tabela))
                    throw new Exception("Tabela não informada!");

                QueryConn01 qcOrigem = new QueryConn01(dbOrigem?.StringConexao, dbOrigem.SGBD, false);
                qcOrigem.Descricao = dbOrigem.Descricao;

                QueryConn01 qcDestino = new QueryConn01(dbDestino?.StringConexao, dbDestino.SGBD, false);
                qcDestino.Descricao = dbDestino.Descricao;

                // Verificar se a tabela existe no banco de dados Origem
                DbConnection.ChecarExistenciaTabela(qcOrigem, tabela);

                // Verificar se a tabela existe no banco de dados Destino
                DbConnection.ChecarExistenciaTabela(qcDestino, tabela);

                // Obtem Query para obter dados da tabela do banco origem
                qcOrigem.GetQueryDadosTabela(tabela, filtro);

                timeIni = DateTime.Now;
                UpdateLog(String.Format("Obtendo dados da tabela {0} de {1}\n", tabela, dbOrigem.Descricao));

                // Obter dados da tabela do banco Origem
                result = DbConnection.Obter(qcOrigem);
                timeFim = DateTime.Now;

                var countRecords = result == null ? 0 : result.Rows.Count;

                UpdateLog(String.Format("Total de {0} registros obtidos. Em {1}\n", countRecords, Utils.ObterDiferencaTempo(timeIni, timeFim)));

                if (countRecords == 0)
                {
                    UpdateLog(String.Format("Não há dados para transferir de {0} para {1}\n", dbOrigem.Descricao, dbDestino.Descricao));
                    return;
                }

                // UpdateLog(String.Format("Inicio Montagem comandos em {0}\n", DateTime.Now));
                var recordOrigem = result?.Copy();
                var colOrigem = recordOrigem?.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

                // Obter lista de ok da tabela Destino
                qcDestino.GetQueryPKTabela(tabela);
                result = DbConnection.Obter(qcDestino);
                var colpks = (from x in result.AsEnumerable() select x[0].ToString()).ToList();

                //var insertCols = colOrigem?.Except(colpks).ToList();

                List<string> lComandoInsert = new();
                List<string> lColValues = new();
                List<string> lComandoUpdate = new();
                StringBuilder q = new StringBuilder();

                List<DbTransf01> dbtransf = new List<DbTransf01>();
                int count = 1;

                if (recordOrigem != null)
                {
                    progressBar1.Maximum = countRecords;
                    lblpgs.Text = "Processando Montagem de Comandos ...";
                    lblpgs.Refresh();

                    foreach (DataRow item in recordOrigem.AsEnumerable())
                    {
                        progressBar1.PerformStep();

                        var dbt = new DbTransf01();

                        // Primary keys 
                        for (int i = 0; i < colpks.Count; i++)
                        {
                            string? value = dbt.GetValue(item, colpks[i], recordOrigem?.Columns[colpks[i]]?.DataType);

                            dbt.PKs?.Add(new DbTransf02()
                            {
                                ColPkName = colpks[i],
                                ColPkValue = value
                            });
                        }

                        q.Clear();

                        if (isInsert)
                        {
                            lColValues.Clear();

                            q.AppendLine(String.Format("INSERT INTO {0}.{1} ({2})", schema, tabela, String.Join(",", colOrigem?.ToArray())));

                            foreach (var col in colOrigem)
                            {
                                lColValues.Add(dbt.GetValue(item, col, recordOrigem?.Columns[col]?.DataType));
                            }

                            q.AppendLine(String.Format("VALUES({0});", string.Join(",", lColValues.ToArray())));

                            lComandoInsert.Add(q.ToString());

                            dbt.ComandoInsert = q.ToString();
                        }

                        q.Clear();

                        if (isUpdate)
                        {
                            count++;
                            var updateCols = colOrigem?.Except(colpks).ToList();

                            q.AppendLine(String.Format("UPDATE {0}.{1} SET", schema, tabela));

                            for (int i = 0; i < updateCols?.Count; i++)
                            {
                                var comma = updateCols.Count <= (1 + i) ? "" : ",";

                                string? value = dbt.GetValue(item, updateCols[i], recordOrigem?.Columns[updateCols[i]]?.DataType);  //"null";

                                q.AppendLine(String.Format(" {0} = {1}{2}", updateCols[i], value, comma));
                            }

                            q.AppendLine(String.Format("WHERE "));

                            for (int i = 0; i < dbt.PKs?.Count; i++)
                            {
                                var colNome = dbt.PKs[i].ColPkName;
                                var value = dbt.PKs[i].ColPkValue;

                                string and = dbt.PKs.Count <= (1 + i) ? ";" : " AND";

                                q.Append(String.Format(" {0} = {1}{2} ", colNome, value, and));
                            }

                            dbt.CommandoUpdate = q.ToString() + String.Format("{0}", count == 3 || count == 6 ? "xx" : "");

                            lComandoUpdate.Add(q.ToString());
                        }

                        dbtransf.Add(dbt);
                    }
                }

                //UpdateLog(String.Format("Finalizou Montagem comandos em {0}\n", DateTime.Now));

                var ini = DateTime.Now;

                UpdateLog(String.Format("Verificando registros...\n"));

                progressBar1.Value = 0;
                progressBar1.Refresh();
                progressBar1.Maximum = dbtransf.Count;
                lblpgs.Text = "Processando Verificação de Registros ...";
                lblpgs.Refresh();

                foreach (var record in dbtransf)
                {
                    progressBar1.PerformStep();

                    // Insert checar se o registro da pk ja existe 
                    // se nao existir insere
                    // se ja existir e update estiver acionado, update

                    StringBuilder selectExist = new StringBuilder();
                    selectExist.AppendLine(String.Format("SELECT COUNT(*) qtd FROM {0}.{1}", schema, tabela));
                    selectExist.Append(String.Format(" WHERE "));

                    for (int i = 0; i < record.PKs?.Count; i++)
                    {
                        var colNome = record.PKs[i].ColPkName;
                        var value = record.PKs[i].ColPkValue;

                        string and = record.PKs.Count <= (1 + i) ? ";" : " AND";

                        selectExist.Append(String.Format("{0} = {1}{2} ", colNome, value, and));
                    }

                    qcDestino.Query = selectExist.ToString();

                    var r = DbConnection.Obter(qcDestino);

                    record.IsExiste = Utils.GetValueDataRow_Int32(r.Rows[0], "qtd", true) > 0;
                }

                var fim = DateTime.Now;
                UpdateLog(String.Format("Tempo Verificação de Registros: {0}\n", Utils.ObterDiferencaTempo(ini, fim)));

                //UpdateLog(String.Format("Total Registro {0}\n", dbtransf.Count));

                List<string>? listInsert = isInsert ? dbtransf.Where(w => !w.IsExiste).Select(s => s.ComandoInsert).ToList() : new List<string?>();

                if (isInsert)
                    UpdateLog(String.Format("Total de Registros para Inserir {0} de {1}\n", listInsert.Count, dbtransf.Count));

                List<string>? listUpdate = isUpdate ? dbtransf.Where<DbTransf01>(w => w.IsExiste).Select<DbTransf01, string>(selector: s => s.CommandoUpdate).ToList<string>() : new List<string>();

                if (isUpdate)
                    UpdateLog(String.Format("Total de Registros para Update {0} de {1}\n", listUpdate.Count, dbtransf.Count));

                var partitionsInsert = listInsert.Partition(regcommit);
                var partitionsUpdate = listUpdate.Partition(regcommit);

                ini = DateTime.Now;

                if (partitionsInsert.Count() == 0 && partitionsUpdate.Count() == 0)
                {
                    UpdateLog(String.Format("Não há dados para atualizar {0}\n", dbDestino.Descricao));
                    return;
                }

                UpdateLog(String.Format("Processando Transferência dos dados em {0}\n", dbDestino.Descricao));

                int numBloco = 0;

                progressBar1.Value = 0;
                progressBar1.Step = regcommit;
                progressBar1.Maximum = (listInsert.Count + listUpdate.Count);
                lblpgs.Text = String.Format("Processando Transferência dos dados em {0} ...", dbDestino.Descricao);
                lblpgs.Refresh();

                if (partitionsInsert.Count() > 0)
                    UpdateLog(String.Format("Processando Comando de 'Inserts' de {0} registros em {1}\n", listInsert.Count, dbDestino.Descricao));

                foreach (List<string> l in partitionsInsert)
                {
                    progressBar1.PerformStep();

                    numBloco++;

                    //UpdateLog(String.Format("Insert Bloco {0}\n", numBloco));

                    qcDestino.ListaQuery = l;
                    qcDestino.NumBloco = numBloco;
                    DbConnection.ExecutarListaDeComandoDbTransf(qcDestino);

                    qcDestino.TotalRegistrosEfetivados += l.Count;

                    if (qcDestino.LQueryExecExcept.Where(w => w.NumBloco == qcDestino.NumBloco).Count() > 0)
                        qcDestino.TotalRegistrosEfetivados = qcDestino.TotalRegistrosEfetivados - l.Count;
                }

                totalIns = qcDestino.TotalRegistrosEfetivados;

                if (partitionsInsert.Count() > 0)
                    UpdateLog(String.Format("Total Insert Efetivados {0} de {1}\n", totalIns, listInsert.Count));

                numBloco = 0;
                qcDestino.TotalRegistrosEfetivados = 0;

                if (partitionsUpdate.Count() > 0)
                    UpdateLog(String.Format("Processando Comando Update de {0} registros em {1} \n", listUpdate.Count, dbDestino.Descricao));


                foreach (List<string> l in partitionsUpdate)
                {
                    progressBar1.PerformStep();

                    numBloco++;
                    // UpdateLog(String.Format("Update Bloco {0}\n", numBloco));
                    qcDestino.ListaQuery = l;
                    qcDestino.NumBloco = numBloco;

                    DbConnection.ExecutarListaDeComandoDbTransf(qcDestino);

                    qcDestino.TotalRegistrosEfetivados += l.Count;

                    if (qcDestino.LQueryExecExcept.Where(w => w.NumBloco == qcDestino.NumBloco).Count() > 0)
                        qcDestino.TotalRegistrosEfetivados = qcDestino.TotalRegistrosEfetivados - l.Count;
                }

                totalUpd = qcDestino.TotalRegistrosEfetivados;

                if (partitionsUpdate.Count() > 0)
                    UpdateLog(String.Format("Total Update Efetivados {0} de {1}\n", totalUpd, listUpdate.Count));


                UpdateLog(String.Format("A Atualização dos dados em {0} foi feita em {1}", dbDestino.Descricao, Utils.ObterDiferencaTempo(ini, DateTime.Now)));

                UpdateLog("===============================================================");

                lblpgs.Text = String.Format("Processamento Finalizado");
                lblpgs.Refresh();

                StringBuilder file = new StringBuilder();

                if (qcDestino.LQueryExecExcept.Count > 0)
                {
                    file.AppendLine(String.Format("\nProcessamento DBTransf em: {0}", DateTime.Now));
                    file.AppendLine(String.Format("Origem: {0}", qcOrigem.Descricao));
                    file.AppendLine(String.Format("Destino: {0}", qcDestino.Descricao));
                    file.AppendLine(String.Format("Schema: {0}, Tabela: {1}", schema, tabela));
                    file.AppendLine(String.Format("Filtro: {0}", filtro));
                    file.AppendLine(String.Format("Total de Registro Origem: {0}", recordOrigem?.Rows.Count));
                    file.AppendLine(String.Format("Total de Registros Para Inserir no Destino: {0}", listInsert.Count));

                    if (isInsert)
                        file.AppendLine(String.Format("Total de Registros Para Inserir Efetivados: {0}", totalIns));
                    file.AppendLine(String.Format("Total de Registros Para Atualizar no Destino: {0}", listUpdate.Count));
                    if (isUpdate)
                        file.AppendLine(String.Format("Total de Registros Para Atualizar Efetivados: {0}", totalUpd));


                    //file.AppendLine(String.Format("{0} de {1} registros inseridos."));
                    file.AppendLine();

                    //file.AppendLine(String.Format("Ocorreu erro na execução dos seguintes comandos: "));
                    foreach (var item in qcDestino.LQueryExecExcept)
                    {
                        file.AppendLine(String.Format("Mensagem: {0}", item.Mensagem));
                        file.AppendLine(String.Format("Comando: {0}", item.Query));

                        file.AppendLine();
                    }

                    Utils.SalvarDados02(file.ToString(), String.Format(@"C:\A9\DbTransf\Logs\Erros"), String.Format("log_error_detail_{0}.txt", DateTime.Now.ToString("yyMMddHHmmss")));
                }

                UpdateLog(file.ToString());
            }
            catch (Exception ex)
            {
                StringBuilder log = new StringBuilder();
                log.AppendLine(ex.Message);

                rchtxtlog.ForeColor = Color.Red;

                rchtxtlog.Text = log.ToString();
                rchtxtlog.SelectionStart = rchtxtlog.Text.Length;
                rchtxtlog.Refresh();
                rchtxtlog.SelectionStart = rchtxtlog.Text.Length;
            }
        }

        private void numCom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((NumericUpDown)sender).Value == 0)
                    ((NumericUpDown)sender).Value = 1;
            }
            catch (Exception)
            {
            }
        }

        private void rchtxtlog_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            rchtxtlog.Undo();
        }
    }
}