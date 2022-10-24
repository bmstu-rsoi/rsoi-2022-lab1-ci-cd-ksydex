using API.Common.AbstractClasses;
using API.Common.Interfaces;

namespace API.Data.Entities;

public class Person : EntityBase
{
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public int Age { get; set; }
}