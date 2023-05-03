using DbTransferWF.Model;
using Newtonsoft.Json;

namespace DbTransferWF
{
    public partial class View_ConfCon : Form
    {
        public View_ConfCon()
        {
            InitializeComponent();

            ConsultarDados();

            cmbSBDB.DataSource = Enum.GetValues(typeof(A9Library.ENUM.EnumSGBD));

            TelaDefault(false);
        }

        private void ConsultarDados()
        {
            try
            {
                List<BancoDados01> l = new List<BancoDados01>();
                //l.Add(new BancoDados01
                //{
                //    Id = DateTime.Now.ToString("yyMMddHHmmss"),
                //    Database = "DBA9",
                //    Descricao = "Banco de dados DBA9 - Produção",
                //    IsCritico = true,
                //    IsProducao = true,
                //    Password = "123456s!",
                //    Port = "5435",
                //    Server = "192.10.20.10",
                //    SGBD = A9Library.ENUM.EnumSGBD.MySQL,
                //    User = "postgres"
                //}) ;

                l.Add(new BancoDados01
                {
                    Id = DateTime.Now.ToString("yyMMddHHmmss"),
                    Database = "DBA9",
                    Descricao = "Banco de dados DBA9 - Desenvolvimento",
                    IsCritico = false,
                    IsProducao = false,
                    Password = "123456s!",
                    Port = "5435",
                    Server = "172.16.20.200",
                    SGBD = A9Library.ENUM.EnumSGBD.PostGreSql,
                    User = "postgres"
                });

                var r = BancoDados01.ObterDadosFromJson();

                if(r!=null)
                    bsGrid.DataSource = r;

                gvDados.Refresh();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            lblAct.Text = "Adicionar uma nova conexão de banco de dados";

            btnEdt.Enabled = btnDel.Enabled = false;
            TelaDefault(true);

            BDFormbs.Add(new BancoDados01());
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TelaDefault(false);
        }

        private void TelaDefault(bool isShowForm)
        {
            if (!isShowForm)
            {
                lblAct.Text = string.Empty;
                btnEdt.Enabled = btnDel.Enabled = btnAdd.Enabled = true;
            }

            gvDados.Enabled = !isShowForm;
          
            this.Size = new Size(this.Size.Width, isShowForm ? 400 : 270);
            
            BDFormbs.Clear();
            
        }
       
        private void BtnSav_Click(object sender, EventArgs e)
        {
            try
            {
                List<BancoDados01>? list = bsGrid.DataSource as List<BancoDados01>;

                list = list ?? new List<BancoDados01>();

                BancoDados01? obj = BDFormbs.Current as BancoDados01;

                if (obj == null) throw new Exception("Não foi possível identificar os dados preenchidos no formulário!");

                string msg = string.Empty;

                obj.Validar(out msg);

                if (!string.IsNullOrEmpty(msg)) throw new Exception(msg);

                if (btnAdd.Enabled)
                {
                    obj.Id = DateTime.Now.ToString("yyMMddHHmmss");

                    list?.Add(obj);

                }
                else if (btnEdt.Enabled)
                {
                    var aux = list?.Where(w => w.Id == obj?.Id).FirstOrDefault();

                    list?.Remove(aux);

                    list?.Add(obj);
                }

                AtualizarDadosConexao(list);

                TelaDefault(false);            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConsultarDados();
            }           
        }

        private void AtualizarDadosConexao(List<BancoDados01> list)
        {
            var x = JsonConvert.SerializeObject(list, Formatting.Indented);

            A9Library.Utils.SalvarDados02(x, @"C:\A9\DBTransf", "conbd.json");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var obj = (gvDados.CurrentRow?.DataBoundItem as BancoDados01)?.Clone();

            if (obj == null)
                MessageBox.Show("Não há dados para editar.");
            else
            {
                lblAct.Text = "Editar conexão de banco de dados";

                btnAdd.Enabled = btnDel.Enabled = false;
                TelaDefault(true);
                BDFormbs.Add(obj);
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            var obj = (gvDados.CurrentRow?.DataBoundItem as BancoDados01);

            if (obj == null)
                MessageBox.Show("Não há dados para excluir.");
            else
            {
                string txt = string.Format("Tem certeza que deseja deletar a conexão {0}?",obj.Descricao);
                string header = "Deletar Conexão";

                var dr = MessageBox.Show(txt, header, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(dr == DialogResult.Yes)
                {
                    List<BancoDados01>? list = bsGrid.DataSource as List<BancoDados01>;

                    list = list ?? new List<BancoDados01>();

                    var aux = list?.Where(w => w.Id == obj?.Id).FirstOrDefault();

                    if (aux != null)
                    {
                        list?.Remove(aux);

                        AtualizarDadosConexao(list);

                        ConsultarDados();

                        MessageBox.Show("Conexão deletada!");
                    }
                }

               
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void gvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
