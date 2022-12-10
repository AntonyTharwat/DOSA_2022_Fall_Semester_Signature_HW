using System;

namespace RestaurantManagementHomeWork4
{
    class reservation
    {
        public TimeSpan eventStartTime { get; set; }
        public TimeSpan eventEndTime { get; set; }
        public table Table { get; set; }
        public person person { get; set; }
    }
}
