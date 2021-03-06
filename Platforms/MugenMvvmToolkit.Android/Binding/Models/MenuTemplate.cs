#region Copyright

// ****************************************************************************
// <copyright file="MenuTemplate.cs">
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
using System.Collections.Generic;
using System.Xml.Serialization;
using Android.Content;
using Android.Views;
using MugenMvvmToolkit.Binding.Builders;
using MugenMvvmToolkit.Binding.Infrastructure;
using MugenMvvmToolkit.Binding.Interfaces;

namespace MugenMvvmToolkit.Binding.Models
{
    [XmlRoot("MENU", Namespace = "")]
    public sealed class MenuTemplate
    {
        #region Properties

        [XmlAttribute("ISENABLED")]
        public string IsEnabled { get; set; }

        [XmlAttribute("ISVISIBLE")]
        public string IsVisible { get; set; }

        [XmlAttribute("DATACONTEXT")]
        public string DataContext { get; set; }

        [XmlAttribute("ITEMSSOURCE")]
        public string ItemsSource { get; set; }

        [XmlElement("ITEMTEMPLATE")]
        public MenuItemTemplate ItemTemplate { get; set; }

        [XmlElement("MENUITEM")]
        public List<MenuItemTemplate> Items { get; set; }

        #endregion

        #region Methods

        public void Apply(IMenu menu, Context context, object parent)
        {
            PlatformExtensions.ValidateTemplate(ItemsSource, Items);
            var setter = new XmlPropertySetter<MenuTemplate, IMenu>(menu, context, new BindingSet());
            BindingExtensions.AttachedParentMember.SetValue(menu, parent);
            setter.SetBinding(template => template.DataContext, DataContext, false);
            setter.SetBoolProperty(template => template.IsVisible, IsVisible);
            setter.SetBoolProperty(template => template.IsEnabled, IsEnabled);
            if (string.IsNullOrEmpty(ItemsSource))
            {
                if (Items != null)
                {
                    for (int index = 0; index < Items.Count; index++)
                        Items[index].Apply(menu, context, index, index);
                }
            }
            else
            {
                MenuItemsSourceGenerator.Set(menu, context, ItemTemplate);
                setter.SetBinding(template => template.ItemsSource, ItemsSource, true);
            }
            setter.Apply();
        }

        public static void Clear(IMenu menu)
        {
            try
            {
                Clear(menu, BindingServiceProvider.BindingManager);
            }
            catch (Exception e)
            {
                Tracer.Error(e.Flatten(true));
            }
        }

        internal static void Clear(IMenu menu, IBindingManager bindingManager)
        {
            if (menu == null)
                return;
            bindingManager.ClearBindings(menu);
            int size = menu.Size();
            for (int i = 0; i < size; i++)
                MenuItemTemplate.Clear(menu.GetItem(i), bindingManager);
            menu.Clear();
            BindingExtensions.AttachedParentMember.SetValue(menu, BindingExtensions.NullValue);
        }

        #endregion
    }
}