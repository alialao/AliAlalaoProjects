using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntranceApp
{
    /// <summary>
    /// visitors model
    /// </summary>
    class visitors
    {
        /// <summary>
        /// Gets or sets visitors id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets visitors firse name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets visitors last name
        /// </summary>
        public string LasttName { get; set; }
        /// <summary>
        /// Gets or sets visitors Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets visitors Phone number
        /// </summary>
        public int Phone { get; set; }
        /// <summary>
        /// Gets or sets visitors Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets visitors postal code
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets visitors deposit
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// Gets or sets visitors Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Gets or sets what kind of camping Individual or Group
        /// </summary>
        public string CampType { get; set; }
    }
}
