using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace SightWordCards
{
    static class Functions
    {
        public static void ExecuteAnimation(UIElement obj, DependencyProperty prop, double init, double term)
        {
            ExecuteAnimation(obj, prop, init, term, TimeSpan.FromMilliseconds(500));
        }

        public static void ExecuteAnimation(UIElement obj, DependencyProperty prop, double init, double term, TimeSpan duration)
        {
            ExecuteAnimation(obj, prop, init, term, duration, new QuarticEase());
        }

        public static void ExecuteAnimation(UIElement obj, DependencyProperty prop, double init, double term, TimeSpan duration, IEasingFunction ease)
        {
            var Anim = new DoubleAnimation() { From = init, To = term, Duration = duration, FillBehavior = FillBehavior.Stop, EasingFunction = ease };
            obj.BeginAnimation(prop, Anim);
        }

        public static void WaitForAnimation(UIElement obj, DependencyProperty prop, double init, double term)
        {
            WaitForAnimation(obj, prop, init, term, TimeSpan.FromMilliseconds(500));
        }

        public static void WaitForAnimation(UIElement obj, DependencyProperty prop, double init, double term, TimeSpan duration)
        {
            WaitForAnimation(obj, prop, init, term, duration, new QuarticEase());
        }

        public async static void WaitForAnimation(UIElement obj, DependencyProperty prop, double init, double term, TimeSpan duration, IEasingFunction ease)
        {
            var Anim = new DoubleAnimation() { From = init, To = term, Duration = duration, FillBehavior = FillBehavior.Stop, EasingFunction = ease };
            obj.BeginAnimation(prop, Anim);
            await Task.Delay(duration);
        }
    }
}