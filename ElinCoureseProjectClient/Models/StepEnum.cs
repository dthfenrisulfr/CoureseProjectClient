using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElinCoureseProjectClient.Models
{
  public enum StepEnum
  {
    getCustomer = 0,
    setCustomerTrue = 1,
    setCustomerFalse = 2,
    customerReadyTrue = 3,
    customerReadyFalse = 4,
    getProducts = 5,
    getStaff = 6,
    getOrder = 7
  }
}
