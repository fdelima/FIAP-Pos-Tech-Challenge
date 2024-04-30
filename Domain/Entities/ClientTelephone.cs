using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities
{
    public partial class ClientTelephone : IDomainEntity
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na inserção.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
        {
            return x => ((ClientTelephone)x).IdClient.Equals(IdClient) &&
                        ((ClientTelephone)x).Number.Equals(Number);
        }

        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
        {
            return x => !((ClientTelephone)x).IdClientTelephone.Equals(IdClientTelephone) &&
                        ((ClientTelephone)x).IdClient.Equals(IdClient) &&
                        ((ClientTelephone)x).Number.Equals(Number);
        }

        public Guid IdClientTelephone { get; set; }

        public Guid IdClient { get; set; }

        public string TelephoneType { get; set; } = null!;

        public string Number { get; set; } = null!;

        [JsonIgnore]
        public virtual Client IdClientNavigation { get; set; } = null!;
    }
}
