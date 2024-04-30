using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities
{
    public partial class ClientAddress : IDomainEntity
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na inserção.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
        {
            return x => ((ClientAddress)x).IdClient.Equals(IdClient) &&
                        ((ClientAddress)x).ZipCode.Equals(ZipCode) &&
                        ((ClientAddress)x).StreetNumber.Equals(StreetNumber);
        }

        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
        {
            return x => !((ClientAddress)x).IdClientAddress.Equals(IdClientAddress) &&
                        ((ClientAddress)x).IdClient.Equals(IdClient) &&
                        ((ClientAddress)x).ZipCode.Equals(ZipCode) &&
                        ((ClientAddress)x).StreetNumber.Equals(StreetNumber);
        }

        public Guid IdClientAddress { get; set; }

        public Guid IdClient { get; set; }

        public string AddressType { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string StreetNumber { get; set; } = null!;

        public string Neighborhood { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        [JsonIgnore]
        public virtual Client IdClientNavigation { get; set; } = null!;
    }
}
