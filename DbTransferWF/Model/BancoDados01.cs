using A9Library.ENUM;
using System.Text;

namespace DbTransferWF.Model
{
    public class BancoDados01
    {
        public string Id { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public bool IsProducao { get; set; }
        public bool IsCritico { get; set; }
        public string Descricao { get; set; }
        public EnumSGBD SGBD { get; set; }
        
        public string StringConexao
        {
            get
            {
                if (SGBD == EnumSGBD.PostGreSql)
                    return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};", this.Server, this.Port, this.Database, this.User, this.Password);
                else
                    return string.Empty;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Validar(out string msg)
        {
            msg = string.Empty;

            StringBuilder str= new StringBuilder();

            if (string.IsNullOrEmpty(this.Descricao))
                str.AppendLine("Descrição deve ser informado!");

            if (string.IsNullOrEmpty(this.Server))
                str.AppendLine("Servidor/Host deve ser informado!");

            if (string.IsNullOrEmpty(this.Database))
                str.AppendLine("Nome do Banco de dados deve ser informado!");

            if (string.IsNullOrEmpty(this.User))
                str.AppendLine("Usuário deve ser informado!");

            if (string.IsNullOrEmpty(this.Password))
                str.AppendLine("Senha deve ser informada!");

            if (string.IsNullOrEmpty(this.Port))
                str.AppendLine("Porta deve ser informada!");

            msg = str.ToString();

        }

        public static List<BancoDados01> ObterDadosFromJson()
        {
            try
            {
                return A9Library.Utils.ObterObjDeArquivoJson<List<BancoDados01>>(@"C:\A9\DBTransf\conbd.json");
            }
            catch (Exception e)
            {

                throw new Exception("ObterDadosFromJson - "+e.Message);
            }
        }
    }
}
