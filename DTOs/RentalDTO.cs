﻿using Vidly.Models;

namespace Vidly.DTOs
{
    public class RentalDTO
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}