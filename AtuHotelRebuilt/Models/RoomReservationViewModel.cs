using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtuHotelRebuilt.Models
{
    public class RoomReservationViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}