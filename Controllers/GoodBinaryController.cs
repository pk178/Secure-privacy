using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure_privacy.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GoodBinaryController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Check(string binary)
        {
            string message = string.Format("Your input '{0}' is NOT a good binrary number", binary);
            try
            {
                // Check number of 0's is equal number of 1's or not
                if (CountNumber(binary, '1') != CountNumber(binary, '0')) return message;

                // Check prefix number
                var totalPrefix = binary.Length;
                for(int i = 0; i < totalPrefix; i++)
                {
                    var prefixNumber = binary.Substring(0, i + 1);

                    // Compare number of 1's is less than number of 0's or not
                    if (CountNumber(prefixNumber, '1') < CountNumber(prefixNumber, '0')) return message;
                }

                return string.Format("Your input '{0}' is a good binrary number", binary);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Check good binary");
            }

            return "Internal error, cannot check your input";
        }

        private int CountNumber(string inputString, char number)
        {
            try
            {
                var inputNumber = inputString.ToArray();
                return inputNumber.Where(x => x == number).Count();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CountNumber");
            }

            return 0;
        }
    }
}
