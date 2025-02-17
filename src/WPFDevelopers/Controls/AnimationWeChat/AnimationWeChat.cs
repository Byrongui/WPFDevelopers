﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WPFDevelopers.Controls
{
    public class AnimationWeChat : Control
    {
        private static Storyboard _storyboard;
        private ObjectAnimationUsingKeyFrames _objectAnimationUsingKeyFrames;
        private UIElement _uIElement;

        public static readonly DependencyProperty DurationProperty =
             DependencyProperty.Register("Duration", typeof(TimeSpan),
             typeof(AnimationWeChat), new PropertyMetadata(null));

        /// <summary>
        /// 动画时间
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty IsLitProperty =
             DependencyProperty.Register("IsLit", typeof(bool),
             typeof(AnimationWeChat), new PropertyMetadata(false, new PropertyChangedCallback(OnIsLitChanged)));

        /// <summary>
        /// 是否开始播放
        /// </summary>
        public bool IsLit
        {
            get { return (bool)GetValue(IsLitProperty); }
            set { SetValue(IsLitProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _uIElement = Template.FindName("animation", this) as UIElement;
            if (_uIElement != null && IsLit)
                Animate(_uIElement);

        }

        private static void OnIsLitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool newValue = (bool)e.NewValue;
            if (newValue)
            {
                AnimationWeChat c = d as AnimationWeChat;
                if (c != null && c._uIElement != null)
                    c.Animate(c._uIElement);
            }
            else
                _storyboard.Stop();
        }

        private void Animate(UIElement animation)
        {
            Storyboard.SetTarget(_objectAnimationUsingKeyFrames, animation);
            Storyboard.SetTargetProperty(_objectAnimationUsingKeyFrames, new PropertyPath(Image.SourceProperty));
            _storyboard.Children.Add(_objectAnimationUsingKeyFrames);
            _storyboard.Begin();
        }
        public AnimationWeChat()
        {
            _storyboard = new Storyboard();
            _objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
            _objectAnimationUsingKeyFrames.FillBehavior = FillBehavior.Stop;
            _objectAnimationUsingKeyFrames.RepeatBehavior = RepeatBehavior.Forever;

            _storyboard.CurrentTimeInvalidated += (s, e) =>
            {
                Clock storyboardClock = (Clock)s;
                if (storyboardClock.CurrentTime >= Duration)
                    IsLit = false;
            };

            _objectAnimationUsingKeyFrames.KeyFrames.Add(
                new DiscreteObjectKeyFrame()
                {
                    Value = new BitmapImage(new Uri("pack://application:,,,/WPFDevelopers;component/Images/AnimationWeChat/0.png")),
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.33))
                });
            _objectAnimationUsingKeyFrames.KeyFrames.Add(
               new DiscreteObjectKeyFrame()
               {
                   Value = new BitmapImage(new Uri("pack://application:,,,/WPFDevelopers;component/Images/AnimationWeChat/1.png")),
                   KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.66))
               });
            _objectAnimationUsingKeyFrames.KeyFrames.Add(
               new DiscreteObjectKeyFrame()
               {
                   Value = new BitmapImage(new Uri("pack://application:,,,/WPFDevelopers;component/Images/AnimationWeChat/2.png")),
                   KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.99))
               });
        }

    }
}

