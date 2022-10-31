using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Common.Interfaces;

public interface IEntity
{
    int Id { get; set; }
}