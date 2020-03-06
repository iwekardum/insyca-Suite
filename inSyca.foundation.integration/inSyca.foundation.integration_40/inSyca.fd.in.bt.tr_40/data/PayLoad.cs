using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSyca.foundation.integration.biztalk.tracking.data
{

    public static class PayLoad
    {
        public static string SearchTimestamp
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    _source = "timestamp",
                    query = new
                    {
                        match_all = new { },
                    },
                    sort = new
                    {
                        timestamp = new
                        {
                            order = "desc"
                        }
                    },
                    size = 1
                });
            }
        }
    }
}


