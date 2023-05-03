// See https://aka.ms/new-console-template for more information
using A9DbConn01;
using A9Library;
using A9Library.DATABASE;
using LibDbTransf;
using System.Data;
using System.Text;

Console.WriteLine("Hello, World!");

try
{
    Console.WriteLine(String.Format("Total {0}", Utils.ObterDiferencaTempo(DateTime.Now, DateTime.Now.AddHours(2).AddMinutes(35))));

    bool isInsert = true;
    bool isUpdate = true;
    string schema = "public";
    
    int qtdCommit = 3;

    string filtro = "where idcidade = 112 limit 10"; //String.Empty;//

    DataTable result;

    string strConnOrigem = string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};", "192.10.20.10", "5435", "DBA9", "postgres", "123456s!");

    string strConnDestino = string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};", "172.16.20.200", "5435", "DBA9", "postgres", "123456s!");
    //string strConnDestino = string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};", "e7290d0ee117.sn.mynetname.net", "1001", "DBA9", "postgres", "123456s!");


    QueryConn01 qcOrigem = new QueryConn01(strConnOrigem, A9Library.ENUM.EnumSGBD.PostGreSql, false);
    qcOrigem.Descricao = "Produção - AM. Transportes";

    QueryConn01 qcDestino = new QueryConn01(strConnDestino, A9Library.ENUM.EnumSGBD.PostGreSql, false);
    qcDestino.Descricao = "Homologação - A9 Informática";

    string tabela = "tb_cep01";

    // Verificar se a tabela existe no banco de dados Origem
    DbConnection.ChecarExistenciaTabela(qcOrigem, tabela);

    // Verificar se a tabela existe no banco de dados Destino
    DbConnection.ChecarExistenciaTabela(qcDestino, tabela);

    // Obtem Query para obter dados da tabela do banco origem
    qcOrigem.GetQueryDadosTabela(tabela, filtro);

    //
    Console.WriteLine(String.Format("Inicio obtençao de dados em {0}", DateTime.Now));
    // Obter dados da tabela do banco Origem
    result = DbConnection.Obter(qcOrigem);
    Console.WriteLine(String.Format("Finalizou obtençao de dados em {0}", DateTime.Now));

    var countRecords = result == null ? 0 : result.Rows.Count;

    Console.WriteLine(String.Format("Quantidade de registros: {0}", countRecords));

    if (countRecords > 0)
    {
        if (isInsert || isUpdate)
        {
            Console.WriteLine(String.Format("Inicio Montagem comandos em {0}", DateTime.Now));
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
                foreach (DataRow item in recordOrigem.AsEnumerable())
                {
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

                        dbt.CommandoUpdate = q.ToString() + String.Format("{0}",count == 3 || count == 6 ? "xx" : "");

                        lComandoUpdate.Add(q.ToString());
                    }

                    dbtransf.Add(dbt);
                }
            }

            Console.WriteLine(String.Format("Finalizou Montagem comandos em {0}", DateTime.Now));

            var ini = DateTime.Now;
            Console.WriteLine(String.Format("Inicio Checar se registro existe {0}", ini));
            foreach (var record in dbtransf)
            {
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
            Console.WriteLine(String.Format("Finalizou Checagem de registro em {0}", fim));

            Console.WriteLine(String.Format("Em {0} segundos.", (fim - ini).TotalSeconds));

            Console.WriteLine(String.Format("Total Registro {0}", dbtransf.Count));

            List<string>? listInsert = dbtransf.Where(w => !w.IsExiste).Select(s => s.ComandoInsert).ToList();
            Console.WriteLine(String.Format("Total Insert {0}", listInsert.Count));

            List<string>? listUpdate = dbtransf.Where<DbTransf01>(w => w.IsExiste).Select<DbTransf01, string>(selector: s => s.CommandoUpdate).ToList<string>();
            Console.WriteLine(String.Format("Total Update {0}", listUpdate.Count));

            var partitionsInsert = listInsert.Partition(qtdCommit);
            var partitionsUpdate = listUpdate.Partition(qtdCommit);

            ini = DateTime.Now;
            Console.WriteLine(String.Format("Inicio Atualização DB {0}", ini));

            int numBloco = 0;

            foreach (List<string> l in partitionsInsert)
            {
                numBloco++;
                Console.WriteLine(String.Format("Insert Bloco {0}", numBloco));

                qcDestino.ListaQuery = l;
                qcDestino.NumBloco = numBloco;
                DbConnection.ExecutarListaDeComando(qcDestino);

                qcDestino.TotalRegistrosEfetivados += l.Count;

                if (qcDestino.LQueryExecExcept.Where(w => w.NumBloco == qcDestino.NumBloco).Count() > 0)
                    qcDestino.TotalRegistrosEfetivados = qcDestino.TotalRegistrosEfetivados - l.Count;
            }

            Console.WriteLine(String.Format("Total Insert Efetivados {0} de {1}", qcDestino.TotalRegistrosEfetivados, listInsert.Count));

            numBloco = 0;
            qcDestino.TotalRegistrosEfetivados = 0;

            foreach (List<string> l in partitionsUpdate)
            {
                numBloco++; 
                Console.WriteLine(String.Format("Update Bloco {0}", numBloco));
                qcDestino.ListaQuery = l;
                qcDestino.NumBloco = numBloco;
               
                DbConnection.ExecutarListaDeComando(qcDestino);

                qcDestino.TotalRegistrosEfetivados += l.Count;

                if (qcDestino.LQueryExecExcept.Where(w => w.NumBloco == qcDestino.NumBloco).Count() > 0)
                    qcDestino.TotalRegistrosEfetivados = qcDestino.TotalRegistrosEfetivados - l.Count;
            }

            Console.WriteLine(String.Format("Total Update Efetivados {0} de {1}", qcDestino.TotalRegistrosEfetivados, listUpdate.Count));


            Console.WriteLine(String.Format("Fim Atualização DB {0} segundos",(DateTime.Now- ini).TotalSeconds));


            StringBuilder file = new StringBuilder();

            if (qcDestino.LQueryExecExcept.Count > 0)
            {
                file.AppendLine(String.Format("Processamento DBTransf em: {0}", DateTime.Now));
                file.AppendLine(String.Format("Origem: {0}", qcOrigem.Descricao));
                file.AppendLine(String.Format("Destino: {0}", qcDestino.Descricao));
                file.AppendLine(String.Format("Schema: {0}, Tabela: {1}", schema, tabela));
                file.AppendLine(String.Format("Filtro: {0}", filtro));
                file.AppendLine(String.Format("Total de Registro Origem: {0}", recordOrigem?.Rows.Count));
                file.AppendLine(String.Format("Total de Registros Para Inserir no Destino: {0}",listInsert.Count));
                file.AppendLine(String.Format("Total de Registros Para Atualizar no Destino: {0}",listUpdate.Count)); ;

                //file.AppendLine(String.Format("{0} de {1} registros inseridos."));
                file.AppendLine();

                file.AppendLine(String.Format("Ocorreu erro na execução dos seguintes comandos: "));
                foreach (var item in qcDestino.LQueryExecExcept)
                {
                    file.AppendLine(String.Format("Mensagem: {0}", item.Mensagem));
                    file.AppendLine(String.Format("Comando: {0}", item.Query));

                    file.AppendLine();
                }
            }

            Console.WriteLine(file.ToString()); 
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

//1.String de conexao

//2. Informar a tabela
//	2.1 Verificar se a tabela existe na base de dados
//	2.2 Executar select

//3. Obter informação de dados
//	3.1 Quantos registros o select retornou

//4. Informar commit bloco  Ex. A cada X registros

//5. Marcar inserir
//	5.1 Verificar quem são as colunas primary keys
//	5.2 Atraves da key verificar se ira inserir ou nao, se a opção alterar estiver marcada, faça update