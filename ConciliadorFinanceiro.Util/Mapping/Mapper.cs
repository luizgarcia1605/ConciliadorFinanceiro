using ConciliadorFinanceiro.Base.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConciliadorFinanceiro.Util.Mapping
{
    public class Mapper
    {
        public static Dictionary<string, string> MapearClasseDB<T>(T objeto, out string campoId)
        {
            try
            {
                campoId = string.Empty;
                var propriedades = typeof(T).GetProperties();
                var objetoSaida = new Dictionary<string, string>();

                foreach (var propriedade in propriedades)
                {
                    var chave = Attribute.GetCustomAttribute(propriedade, typeof(KeyAttribute)) as KeyAttribute;

                    if (chave != null)
                        campoId = propriedade.Name;

                    if ((!String.IsNullOrEmpty(propriedade.Name)))
                    {
                        var valor = propriedade.GetValue(objeto);

                        if (valor != null)
                        {
                            var adicionar = false;

                            if (propriedade.PropertyType == typeof(string))
                            {
                                if (!String.IsNullOrEmpty(valor.ToString()))
                                {
                                    valor = "'" + valor.ToString().Replace("'", "''") + "'";
                                    adicionar = true;
                                }
                            }

                            else if (propriedade.PropertyType == typeof(DateTime))
                            {
                                valor = "'" + Convert.ToDateTime(valor).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                adicionar = true;
                            }

                            else if (propriedade.PropertyType == typeof(bool))
                            {
                                valor = ((bool)valor == true) ? "1" : "0";
                                adicionar = true;
                            }

                            else
                            {
                                valor = valor.ToString().Replace(".", "").Replace(",", ".");
                                adicionar = true;
                            }

                            if (adicionar)
                                objetoSaida.Add(propriedade.Name, valor.ToString());
                        }
                    }
                }

                return objetoSaida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
