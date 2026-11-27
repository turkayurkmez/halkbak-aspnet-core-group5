using System;

namespace ECommerce.Domain;

public class Customer
{
  public int Id { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } =string.Empty;

  public virtual List<Order> Orders { get; set; } = [];



}
