using System;

namespace RestaurantManagementHomeWork4
{
    class reservation
    {
        // Automatic Properties (Short Hand) for get and st each attribute
        public TimeSpan eventStartTime { get; set; }
        public TimeSpan eventEndTime { get; set; }
        public table Table { get; set; }
        public person person { get; set; }
    }
}
