namespace RestaurantManagementHomeWork4
{
    enum availabletables
    {
        T1, T2, T3, T4, T5, T6, T7
    }
    class table
    {
        // Automatic Properties (Short Hand) for get and st each attribute
        public availabletables tableNumber { get; set; }
        public int numberOfChairs { get; set; } 
    }
}
