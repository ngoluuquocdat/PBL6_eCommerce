using System;

namespace eComSolution.Data.Entities
{
    public class ResetPass
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Time { get; set; }
        public int Numcheck { get; set; }

    }
}