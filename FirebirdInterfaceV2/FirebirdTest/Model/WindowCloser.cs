using System.Windows;

namespace FirebirdTest.Model
{
    internal class WindowCloser
    {
        public static DependencyProperty EnableWindowClosingProperty { get; private set; }

        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }
    }
}
