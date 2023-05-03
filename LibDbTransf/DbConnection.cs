using A9Library;
using A9Library.DATABASE;
using A9Library.ENUM;
using Npgsql;
using System.Data;
using System.Text;

namespace LibDbTransf
{
    public class DbConnection
    {
        private DbConnection()
        {
        }

        #region Obtem Resultado SQL
        public static DataTable Obter(QueryConn01 qconn)
        {
            qconn.Validar();

            if (qconn.IsGetStrConnFromFile)
                qconn.StrConn = GetConnectionString(qconn.SourceFile);

            if (EnumSGBD.PostGreSql == qconn.Sgbd)
                return PostGreSql.ObterResultSQL(qconn.Query, qconn.StrConn);

            throw new Exception("Não há codificação para conexão de base de dados para o SGBD " + qconn.Sgbd.ToString());
        }

        public static DataTable ObterExpressao(QueryConn01 qconn, String idsql)
        {
            if (string.IsNullOrEmpty(idsql)) throw new Exception("Não foi possível identificar o código do idsql para obter expressão!");

            qconn.Query = "Select * FROM tb_expressao WHERE idsql = " + idsql;

            var r = Obter(qconn);

            if (r == null || r.Rows.Count == 0) throw new Exception(string.Format("A expressão com idsql = {0} não foi encontrada na base de dados!", idsql));

            return r;
        }

        public static String ObterDsComandoExpressao(QueryConn01 qconn, String idsql)
        {
            var r = ObterExpressao(qconn, idsql);

            string dscomando = Utils.GetValueDataRow_String(r.Rows[0], "dscomando", true);

            return dscomando;
        }
       
        #endregion

        public static void ExecutarComando(QueryConn01 qConn)
        {
            qConn.Validar();

            if (qConn.IsGetStrConnFromFile)
                qConn.StrConn = GetConnectionString(qConn.SourceFile);

            if (EnumSGBD.PostGreSql == qConn.Sgbd)
                PostGreSql.ExecutarComando(qConn.Query, qConn.StrConn);
            else
                throw new Exception("Não há codificação para conexão de base de dados para o SGBD " + qConn.Sgbd.ToString());
        }

        public static void ExecutarListaDeComando(QueryConn01 qConn)
        {
            try
            {
                qConn.Validar(isListQuery: true);

                if (qConn.IsGetStrConnFromFile)
                    qConn.StrConn = GetConnectionString(qConn.SourceFile);

                if (qConn.ListaQuery.Count == 1)
                {
                    if (EnumSGBD.PostGreSql == qConn.Sgbd)
                        PostGreSql.ExecutarComando(qConn.ListaQuery[0], qConn.StrConn);
                    else
                        throw new Exception("Não há codificação para conexão de base de dados para o SGBD " + qConn.Sgbd.ToString());

                }
                else
                {
                    if (EnumSGBD.PostGreSql == qConn.Sgbd)
                        PostGreSql.ExecutarListaComando(qConn.ListaQuery, qConn.StrConn);
                    else
                        throw new Exception("Não há codificação para conexão de base de dados para o SGBD " + qConn.Sgbd.ToString());

                }

            }
            catch (Exception ex)
            {
                String str = string.Empty;
                if (ex.InnerException != null)
                    str = ex.InnerException.Message;

                if (!str.Contains(ex.Message))
                    str += " " + ex.Message;

                throw new Exception("Obter /" + str);
            }
        }


        public static object ExecutarComandoComArquivo(QueryConn01 qConn, byte[] arquivo)
        {
            object obj;

            try
            {
                qConn.Validar();

                if (qConn.IsGetStrConnFromFile)
                    qConn.StrConn = GetConnectionString(qConn.SourceFile);

                if (EnumSGBD.PostGreSql == qConn.Sgbd)
                    obj = PostGreSql.ExecutarComandoComArquivo(qConn.Query, qConn.StrConn, arquivo);
                else
                    throw new Exception("Não há codificação para conexão de base de dados para o SGBD " + qConn.Sgbd.ToString());
            }
            catch (Exception ex)
            {
                String str = string.Empty;
                if (ex.InnerException != null)
                    str = ex.InnerException.Message;

                if (!str.Contains(ex.Message))
                    str += " " + ex.Message;

                throw new Exception("ExecutarComandoComArquivo /" + str);
            }

            return obj;
        }


        public static String RegistrarApiLogErroQuery(Exception ex, string comando, string controller, string servico, Int32? iduser, String path, bool isGetStrConnFile = true)
        {
            try
            {
                if (isGetStrConnFile)
                {
                    String strConn = GetConnectionString(path);

                    string codigo = Utils.GetValueSectionSetting(path, "sgbd") ?? throw new ArgumentException("Código SGBD não identificado no arquivo configuracao ini!");

                    EnumSGBD sgbd = (EnumSGBD)Convert.ToInt32(codigo);

                    return RegistrarApiLogErroQuery(ex, comando, controller, servico, iduser, strConn);
                }

                return RegistrarApiLogErroQuery(ex, comando, controller, servico, iduser, path);
            }
            catch (Exception)
            {
                return Utils.MyCustomMsgFromException(ex);
            }
        }

        public static String RegistrarApiLogErroQuery(Exception ex, string comando, string controller, string servico, Int32? iduser, String strConn)
        {
            try
            {
                if (ex is Npgsql.NpgsqlException)
                    return string.Format(
                        "{0}",
                        PostGreSql.RegistrarLogErroERetornarCodigo(ex as PostgresException, comando, iduser, controller, servico, strConn));

                return Utils.MyCustomMsgFromException(ex);
            }
            catch (Exception)
            {
                return Utils.MyCustomMsgFromException(ex);
            }
        }

        public static String RegistrarApiLogErroException(Exception ex, string controller, string servico, Int32? iduser, string path)
        {
            try
            {
                String strConn = GetConnectionString(path);

                string codigo = Utils.GetValueSectionSetting(path, "sgbd") ?? throw new ArgumentException("Código SGBD não identificado no arquivo configuracao ini!");

                EnumSGBD sgbd = (EnumSGBD)Convert.ToInt32(codigo);

                return string.Format(
                        "Código do erro {0}", RegistrarApiLogErroException(ex, controller, servico, iduser, strConn, sgbd));
            }
            catch (Exception)
            {
                return Utils.MyCustomMsgFromException(ex);
            }
        }

        public static void RegistrarApiLogFilhoErroException(string mensagem, string controller, string servico, Int32? iduser, string path, string codigopai)
        {
            try
            {
                String strConn = GetConnectionString(path);

                string codigo = Utils.GetValueSectionSetting(path, "sgbd") ?? throw new ArgumentException("Código SGBD não identificado no arquivo configuracao ini!");

                EnumSGBD sgbd = (EnumSGBD)Convert.ToInt32(codigo);

                RegistrarApiLogFilhoErroException(mensagem, controller, servico, iduser, strConn, sgbd, codigopai);
            }
            catch (Exception)
            {
                // return Utils.MyCustomMsgFromException(ex);
            }
        }

        public static String RegistrarApiLogFilhoErroException(String msg, string controller, string servico, Int32? iduser, String strConn, EnumSGBD sgbd, string codigopai)
        {
            try
            {
                if (sgbd == EnumSGBD.PostGreSql)
                    return string.Format("{0}", PostGreSql.RegistrarApiLog(msg.ToString(), iduser, controller, servico, 1, strConn, codigopai));

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static String RegistrarApiLogErroException(Exception ex, string controller, string servico, Int32? iduser, String strConn, EnumSGBD sgbd)
        {
            try
            {
                StringBuilder msg = new();
                if (ex.InnerException != null)
                    msg.AppendLine(ex.InnerException.Message);

                if (!msg.ToString().Contains(ex.Message))
                    msg.AppendLine(ex.Message);

                msg.AppendLine();
                msg.AppendLine(ex.StackTrace);

                if (sgbd == EnumSGBD.PostGreSql)
                    return string.Format("{0}", PostGreSql.RegistrarApiLog(msg.ToString(), iduser, controller, servico, 1, strConn));

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static String RegistrarApiLog(String mensagem, string controller, string servico, Int32? iduser, short flagErro, QueryConn01 qConn)
        {
            try
            {
                String strConn = qConn.IsGetStrConnFromFile ? GetConnectionString(qConn.SourceFile) : qConn.StrConn;

                if (qConn.Sgbd == EnumSGBD.PostGreSql)
                    return string.Format("{0}", PostGreSql.RegistrarApiLog(mensagem, iduser, controller, servico, flagErro, strConn));

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static String GetConnectionString(String path)
        {
            try
            {
                IniParser parser = new IniParser(path);

                String ambiente;

                ambiente = parser.GetSetting("configuracoes", "ambiente_banco");

                if (string.IsNullOrEmpty(ambiente))
                    throw new Exception("Não foi possível identificar no arquivo " + path + " de configuração em qual ambiente de banco de dados acessar (teste | homologação | produção)!");


                switch (ambiente.ToLower())
                {
                    case "teste":
                    case "homologacao":
                    case "producao":
                        return GetConnStrAmbV2(ambiente, parser);
                    default:
                        throw new Exception("Valor informado no arquivo de configuração 'a9conf' na propriedade ambiente_banco é inválido [" + ambiente + "]. Deve ser informado (teste ou homologacao ou producao)!");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetConnectionString / " + e.Message);
            }

        }

        private static String GetConnStrAmb(string ambiente, IniFile ini)
        {
            try
            {
                if (ini == null)
                    throw new Exception("Não foi possível identificar o arquivo de configuração '.ini'");


                if (string.IsNullOrEmpty(ambiente))
                    throw new Exception("Não foi possível identificar no arquivo de configuração em qual ambiente de banco de dados acessar (teste | homologação | produção)!");

                ambiente = ambiente.ToLower();

                string servidor = ini.IniReadValue("configuracoes", "servidor_database_" + ambiente);
                string porta = ini.IniReadValue("configuracoes", "porta_" + ambiente);
                string database = ini.IniReadValue("configuracoes", "database_" + ambiente);
                string usuario = ini.IniReadValue("configuracoes", "usuario_database_" + ambiente);
                string senha = ini.IniReadValue("configuracoes", "senha_database_" + ambiente);

                return "Server=" + servidor + ";Port=" + porta + ";Database=" + database + ";User Id=" + usuario + ";Password=" + senha + ";CommandTimeout=300";
            }
            catch (Exception e)
            {
                throw new Exception("GetConnStrAmb / " + e.Message);
            }
        }

        private static String GetConnStrAmbV2(string ambiente, IniParser ini)
        {
            try
            {
                if (ini == null)
                    throw new Exception("Não foi possível identificar o arquivo de configuração '.ini'");


                if (string.IsNullOrEmpty(ambiente))
                    throw new Exception("Não foi possível identificar no arquivo de configuração em qual ambiente de banco de dados acessar (teste | homologação | produção)!");

                ambiente = ambiente.ToLower();

                string servidor = ini.GetSetting("configuracoes", "servidor_database_" + ambiente);
                string porta = ini.GetSetting("configuracoes", "porta_" + ambiente);
                string database = ini.GetSetting("configuracoes", "database_" + ambiente);
                string usuario = ini.GetSetting("configuracoes", "usuario_database_" + ambiente);
                string senha = ini.GetSetting("configuracoes", "senha_database_" + ambiente);

                return "Server=" + servidor + ";Port=" + porta + ";Database=" + database + ";User Id=" + usuario + ";Password=" + senha + ";CommandTimeout=300";
            }
            catch (Exception e)
            {
                throw new Exception("GetConnStrAmbV2 / " + e.Message);
            }
        }

        private class PostGreSql
        {
            private PostGreSql()
            {

            }

            public static DataTable ObterResultSQL(String sql, String strConn)
            {
                try
                {
                    DataTable resultado = new DataTable();

                    using (NpgsqlConnection conexao = new NpgsqlConnection(strConn))
                    {
                        if (conexao.State != ConnectionState.Open)
                            conexao.Open();

                        using (NpgsqlDataAdapter adpt = new NpgsqlDataAdapter(sql, conexao))
                        {
                            adpt.Fill(resultado);
                        }

                        conexao.Close();
                    }

                    return resultado;
                }
                catch (NpgsqlException ex)
                {
                    // Criar Log aqui
                    //String str = string.Empty;
                    //if (ex.InnerException != null)
                    //    str = ex.InnerException.Message;

                    //if (!str.Contains(ex.Message))
                    //    str += " " + ex.Message;

                    throw ex;
                }
                catch (Exception e)
                {
                    String str = string.Empty;
                    if (e.InnerException != null)
                        str = e.InnerException.Message;

                    if (!str.Contains(e.Message))
                        str += " " + e.Message;

                    throw new Exception("Obter /" + str);
                }

            }

            public static void ExecutarComando(String cmd, String strConn)
            {
                NpgsqlTransaction transacao;

                try
                {
                    using (NpgsqlConnection conexao = new NpgsqlConnection(strConn))
                    {
                        if (conexao.State != ConnectionState.Open)
                            conexao.Open();

                        transacao = conexao.BeginTransaction();

                        using (NpgsqlCommand command = new NpgsqlCommand(cmd, conexao))
                        {
                            try
                            {
                                command.ExecuteNonQuery();
                                transacao.Commit();
                                conexao.Close();
                            }
                            catch (NpgsqlException e)
                            {
                                transacao.Rollback();
                                conexao.Close();
                                throw e;
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    // Criar Log aqui
                    throw ex;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static object ExecutarComandoComArquivo(String cmd, String strConn, byte[] arquivo)
            {
                NpgsqlTransaction transacao;

                object obj;

                try
                {
                    using (NpgsqlConnection conexao = new NpgsqlConnection(strConn))
                    {
                        if (conexao.State != ConnectionState.Open)
                            conexao.Open();

                        transacao = conexao.BeginTransaction();


                        using (NpgsqlCommand command = new NpgsqlCommand(cmd, conexao))
                        {
                            try
                            {
                                NpgsqlParameter param = command.Parameters.Add("@anexo", NpgsqlTypes.NpgsqlDbType.Bytea);
                                param.Value = arquivo;

                                obj = command.ExecuteScalar();
                                transacao.Commit();
                                conexao.Close();
                            }
                            catch (NpgsqlException e)
                            {
                                transacao.Rollback();
                                conexao.Close();
                                throw e;
                            }
                        }
                    }

                    return obj;
                }
                catch (NpgsqlException ex)
                {
                    // Criar Log aqui
                    throw ex;
                }
                catch (Exception)
                {
                    throw;
                }
            }


            public static void ExecutarListaComando(List<String> lCmd, String strConn)
            {
                NpgsqlTransaction transacao;

                try
                {
                    using (NpgsqlConnection conexao = new NpgsqlConnection(strConn))
                    {
                        if (conexao.State != ConnectionState.Open)
                            conexao.Open();

                        transacao = conexao.BeginTransaction();

                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {

                            try
                            {
                                command.Connection = conexao;

                                foreach (var cmd in lCmd)
                                {
                                    command.CommandText = cmd;
                                    command.ExecuteNonQuery();

                                }

                                transacao.Commit();
                                conexao.Close();
                            }
                            catch (NpgsqlException e)
                            {
                                transacao.Rollback();
                                conexao.Close();
                                throw e;
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    // Criar Log aqui
                    String str = string.Empty;
                    if (ex.InnerException != null)
                        str = ex.InnerException.Message;

                    if (!str.Contains(ex.Message))
                        str += " " + ex.Message;

                    throw new Exception("ExecutarListaComando /" + str);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static String RegistrarLogErroERetornarCodigo(PostgresException postgresException, String comando, int? iduser, String controller, String servico, String strConn)
            {
                try
                {
                    NpgSqlPostgresExceptionAux r = new NpgSqlPostgresExceptionAux();
                    r.BaseMessage = postgresException?.MessageText;
                    r.CodeError = postgresException?.SqlState;
                    r.Message = postgresException?.Message;
                   // r.Sql = postgresException?.Statement?.SQL;

                    StringBuilder msg = new StringBuilder();
                    msg.AppendLine("Mensagem Base: " + r.BaseMessage);
                    msg.AppendLine("Código de erro SGBD: " + r.CodeError);
                    msg.AppendLine("Messagem: " + r.Message);
                    if (postgresException?.InnerException != null)
                        msg.AppendLine("Inner Exception: " + postgresException.InnerException.Message);

                    msg.AppendLine("-- Comando --");
                    msg.AppendLine(comando);
                    msg.AppendLine();
                    msg.AppendLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                    msg.AppendLine(postgresException?.StackTrace);

                    var codigo = RegistrarApiLog(msg.ToString(), iduser, controller, servico, 1, strConn);

                    if (codigo == null)
                        return string.Empty;

                    return string.Format("{0}", codigo);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public static Int64? RegistrarApiLog(string mensagem, int? idUser, string controller, string servico, short flagErro, String strConn, string codigopai = "")
            {
                try
                {
                    if (string.IsNullOrEmpty(mensagem))
                        throw new Exception("Não há mensagem de log!");

                    mensagem = mensagem.Replace("'", "''");

                    Int64? codigo = null;

                    StringBuilder cmd = new StringBuilder();
                    cmd.AppendLine("INSERT INTO tb_log_api01( ");
                    cmd.AppendLine("        mensagem");
                    cmd.AppendLine("        ,controller");
                    cmd.AppendLine("        ,servico");
                    cmd.AppendLine("        ,flagerro");
                    cmd.AppendLine("        ,iduser");
                    cmd.AppendLine("        ,idlogpai");
                    cmd.AppendLine("        ,dtevento)");
                    cmd.AppendLine("       Values(");
                    cmd.AppendLine("       ':p_mensagem'");
                    cmd.AppendLine("       ,':p_controller'");
                    cmd.AppendLine("       ,':p_servico'");
                    cmd.AppendLine("       ,:p_erro");
                    cmd.AppendLine("       ,:p_iduserapi");
                    cmd.AppendLine("       ,:p_paicodigo");
                    cmd.AppendLine("       ,now()) returning idlog");

                    cmd.Replace(":p_erro", string.Format("{0}", flagErro));
                    cmd.Replace(":p_codigo", string.Format("{0}", codigo));
                    cmd.Replace(":p_paicodigo", string.Format("{0}", string.IsNullOrEmpty(codigopai) ? "NULL" : codigopai));
                    cmd.Replace(":p_controller", controller ?? string.Empty);
                    cmd.Replace(":p_mensagem", mensagem ?? string.Empty);
                    cmd.Replace(":p_servico", servico ?? string.Empty);
                    cmd.Replace(":p_iduserapi", string.Format("{0}", idUser ?? 0));

                    using (NpgsqlConnection conexao = new NpgsqlConnection(strConn))
                    {
                        if (conexao.State != ConnectionState.Open)
                            conexao.Open();

                        using (NpgsqlCommand command = new NpgsqlCommand(cmd.ToString(), conexao))
                        {
                            var cod = command.ExecuteScalar();

                            codigo = Convert.ToInt64(cod);
                            //command.ExecuteNonQuery();
                        }

                        conexao.Close();
                    }

                    return codigo;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    return null;
                }
            }

        }

    }
}