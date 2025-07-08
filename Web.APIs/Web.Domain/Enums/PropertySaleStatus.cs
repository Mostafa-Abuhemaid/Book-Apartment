using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public  enum PropertySaleStatus
    {
        Available,
        Waiting_for_reply,
        UnderReview,
        Sold
    }
}
