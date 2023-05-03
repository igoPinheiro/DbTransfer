using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LibDbTransf
{
    public class DbTransf01
    {
        public String? Descricao { get; set; }
        public String? Msg { get; set; }
        public String? ComandoInsert { get; set; }
        public String? CommandoUpdate { get; set; }
        public List<DbTransf02>? PKs { get; set; }
        public Boolean IsExiste { get; set; }

        public DbTransf01()
        {
            PKs = new List<DbTransf02>();
        }

        public string? GetValue(DataRow? row, string col, Type type)
        {
            string? value = "null";

            if (row?[col] != DBNull.Value)
            {
                string? _type =type.FullName;

                if (_type != null && (_type.ToLower().Contains("string") || _type.ToLower().Contains("date")))
                    value = string.Format("'{0}'", row?[col].ToString());
                else
                    value = row?[col].ToString()?.Replace(",", ".");
            }

            return value;
        }
    }

    public class DbTransf02
    {
        public string? ColPkName { get; set; }
        public string? ColPkValue { get; set; }
    }
}
