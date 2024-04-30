using FIAP.Pos.Tech.Challenge.Domain.Entities;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Domain.Extensions
{
    /// <summary>
    /// Extensão da model para informar os campos de validação.
    /// </summary>
    public static class ClientExtension
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public static Expression<Func<Client, bool>> ConsultRule(this PagingQueryParam<Client> param)
        {
            return x => (x.IdClient.Equals(param.ObjFilter.IdClient) || param.ObjFilter.IdClient.Equals(default)) &&
                        (x.Name.Contains(param.ObjFilter.Name) || string.IsNullOrWhiteSpace(param.ObjFilter.Name)) &&
                        (x.LastName.Contains(param.ObjFilter.LastName) || string.IsNullOrWhiteSpace(param.ObjFilter.LastName)) &&
                        (x.LastName.Contains(param.ObjFilter.Email) || string.IsNullOrWhiteSpace(param.ObjFilter.Email)) &&
                        (x.NumberDocument.Equals(param.ObjFilter.NumberDocument) || param.ObjFilter.NumberDocument == null || param.ObjFilter.NumberDocument.Equals(default)) &&
                        (x.BirthDate.Equals(param.ObjFilter.BirthDate) || param.ObjFilter.BirthDate == null || param.ObjFilter.BirthDate.Equals(default));



        }

        /// <summary>
        /// Retorna a propriedade a ser ordenada
        /// </summary>
        public static Expression<Func<Client, object>> SortProp(this PagingQueryParam<Client> param)
        {
            switch (param?.SortProperty?.ToLower())
            {
                case "idclient":
                    return fa => fa.IdClient;
                case "lastname":
                    return fa => fa.LastName;
                case "email":
                    return fa => fa.Email;
                case "numberdocument":
                    return fa => fa.NumberDocument;
                case "birthdate":
                    return fa => fa.BirthDate;
                default: return fa => fa.Name;
            }
        }
    }
}
