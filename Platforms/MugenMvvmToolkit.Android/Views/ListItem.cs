#region Copyright

// ****************************************************************************
// <copyright file="ListItem.cs">
// Copyright (c) 2012-2015 Vyacheslav Volkov
// </copyright>
// ****************************************************************************
// <author>Vyacheslav Volkov</author>
// <email>vvs0205@outlook.com</email>
// <project>MugenMvvmToolkit</project>
// <web>https://github.com/MugenMvvmToolkit/MugenMvvmToolkit</web>
// <license>
// See license.txt in this solution or http://opensource.org/licenses/MS-PL
// </license>
// ****************************************************************************

#endregion

using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using MugenMvvmToolkit.Binding;

namespace MugenMvvmToolkit.Views
{
    public class ListItem : FrameLayout, ICheckable
    {
        #region Fields

        private bool _checked;
        private readonly int _templateId;
        private object _dataContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListItem" /> class.
        /// </summary>
        public ListItem(int templateId, LayoutInflater inflater)
            : base(inflater.Context)
        {
            _templateId = templateId;
            inflater.CreateBindableView(templateId, this, true);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the id of template.
        /// </summary>
        public int TemplateId
        {
            get { return _templateId; }
        }

        /// <summary>
        ///     Gets the first child.
        /// </summary>
        protected View FirstChild
        {
            get
            {
                if (ChildCount == 0)
                    return null;
                View firstChild = GetChildAt(0);
                return firstChild;
            }
        }

        //NOTE ConditionalWeakTable invokes finalizer for value, even if the key object is still alive https://bugzilla.xamarin.com/show_bug.cgi?id=21620
        public object DataContext
        {
            get { return _dataContext; }
            set
            {
                if (Equals(value, _dataContext))
                    return;
                _dataContext = value;
                var eventHandler = DataContextChanged;
                if (eventHandler != null)
                    eventHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Occurs when the DataContext property changed.
        /// </summary>
        public event EventHandler DataContextChanged;

        #endregion

        #region Implementation of ICheckable

        public virtual void Toggle()
        {
            var contentCheckable = FirstChild as ICheckable;
            if (contentCheckable == null)
                _checked = !_checked;
            else
                contentCheckable.Toggle();
        }

        public virtual bool Checked
        {
            get
            {
                var contentCheckable = FirstChild as ICheckable;
                if (contentCheckable == null)
                    return _checked;

                return contentCheckable.Checked;
            }
            set
            {
                var contentCheckable = FirstChild as ICheckable;
                if (contentCheckable == null)
                {
                    _checked = value;
                    TrySetActivated(FirstChild, value);
                }
                else
                    contentCheckable.Checked = value;
            }
        }

        #endregion

        #region Methods

        private static void TrySetActivated(View view, bool value)
        {
            if (view == null || Build.VERSION.SdkInt <= BuildVersionCodes.GingerbreadMr1)
                return;
            var type = view.GetType();
            var member = BindingServiceProvider.MemberProvider.GetBindingMember(type, "Activated", false, false);
            if (member != null && member.CanWrite)
                member.SetValue(view, new[] { Empty.BooleanToObject(value) });
        }

        #endregion
    }
}