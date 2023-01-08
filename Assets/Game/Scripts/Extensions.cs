public static class Extensions
{
    public static Signs GetOtherSign(this Signs sign) => sign == Signs.O ? Signs.X : Signs.O;
}