using System.ComponentModel.DataAnnotations;
using API.Common.Interfaces;

namespace API.Common.AbstractClasses;

public abstract class EntityBase : IEntity
{
    [Key] public int Id { get; set; }
}