using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities
{
    public partial class Client : IDomainEntity
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na inserção.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
        {
            return x => ((Client)x).Name.Trim().Equals(Name);
        }

        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
        {
            return x => !((Client)x).IdClient.Equals(IdClient) &&
                        ((Client)x).Name.Trim().Equals(Name);
        }

        public Guid IdClient { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public long NumberDocument { get; set; }

        [NotMapped]
        public string NumberDocumentFormatted
        {
            get
            {
                string texto = $"{NumberDocument:000-00}";
                return $"XXX.XXX.{texto.Substring(texto.Length - 6)}";
            }
        }

        public DateTime? BirthDate { get; set; }

        public string Status { get; set; } = null!;

        public string? Gender { get; set; }

        [JsonIgnore]
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; } = new List<ClientAddress>();

        [JsonIgnore]
        public virtual ICollection<ClientTelephone> ClientTelephones { get; set; } = new List<ClientTelephone>();

    }
}
