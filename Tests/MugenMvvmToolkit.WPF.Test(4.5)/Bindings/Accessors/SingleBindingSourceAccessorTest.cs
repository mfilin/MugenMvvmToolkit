﻿using System;
using System.Globalization;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MugenMvvmToolkit.Binding;
using MugenMvvmToolkit.Binding.Accessors;
using MugenMvvmToolkit.Binding.DataConstants;
using MugenMvvmToolkit.Binding.Infrastructure;
using MugenMvvmToolkit.Binding.Interfaces.Accessors;
using MugenMvvmToolkit.Binding.Models;
using MugenMvvmToolkit.Binding.Sources;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.Test.TestInfrastructure;
using MugenMvvmToolkit.Test.TestModels;
using MugenMvvmToolkit.ViewModels;
using Should;

namespace MugenMvvmToolkit.Test.Bindings.Accessors
{
    [TestClass]
    public class SingleBindingSourceAccessorTest : BindingTestBase
    {
        #region Methods

        [TestMethod]
        public void GetValueShouldReturnActualValue()
        {
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.IntProperty);
            sourceModel.IntProperty = int.MaxValue;
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.IntProperty);
        }

        [TestMethod]
        public void GetValueShouldReturnActualValueIndexer()
        {
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model["test"]);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel["test"]);
            sourceModel["test"] = propertyName;
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(propertyName);
        }

        [TestMethod]
        public void GetValueShouldReturnActualValueDoubleIndexer()
        {
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model["test", 0]);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel["test", 0]);
            sourceModel["test", 0] = propertyName;
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(propertyName);
        }

        [TestMethod]
        public void GetValueShouldReturnDefaultValueIfNestedPropertyNull()
        {
            var memberMock = new BindingMemberInfoMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel.IntProperty);
            var sourceModel = new BindingSourceModel();
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(null);

            memberMock.Type = typeof(int);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(0);
        }

        [TestMethod]
        public void GetValueShouldReturnDefaultValueIfNestedPropertyNullIndexer()
        {
            var memberMock = new BindingMemberInfoMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test"]);
            var sourceModel = new BindingSourceModel();
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(null);

            memberMock.Type = typeof(int);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(0);
        }

        [TestMethod]
        public void GetValueShouldReturnDefaultValueIfNestedPropertyNullDoubleIndexer()
        {
            var memberMock = new BindingMemberInfoMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test", 0]);
            var sourceModel = new BindingSourceModel();
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(null);

            memberMock.Type = typeof(int);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(0);
        }

        [TestMethod]
        public void GetValueShouldReturnActualValueForNestedProperty()
        {
            var memberMock = new BindingMemberInfoMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel.IntProperty);
            var sourceModel = new BindingSourceModel { NestedModel = new BindingSourceNestedModel() };
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.NestedModel.IntProperty);
            sourceModel.NestedModel.IntProperty = int.MaxValue;
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.NestedModel.IntProperty);
        }

        [TestMethod]
        public void GetValueShouldReturnActualValueForNestedPropertyIndexer()
        {
            var memberMock = new BindingMemberInfoMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test"]);
            var sourceModel = new BindingSourceModel { NestedModel = new BindingSourceNestedModel() };
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.NestedModel["test"]);
            sourceModel.NestedModel["test"] = propertyName;
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.NestedModel["test"]);
        }

        [TestMethod]
        public void GetValueShouldReturnActualValueForNestedPropertyDoubleIndexer()
        {
            var memberMock = new BindingMemberInfoMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test", 0]);
            var sourceModel = new BindingSourceModel { NestedModel = new BindingSourceNestedModel() };
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.NestedModel["test", 0]);
            sourceModel.NestedModel["test", 0] = propertyName;
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(sourceModel.NestedModel["test", 0]);
        }

        [TestMethod]
        public void GetValueShouldThrowExceptionInvalidValueIfFlagTrue()
        {
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel();
            const string propertyName = "invalid";
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            ShouldThrow(() => valueAccessor.GetValue(memberMock, EmptyContext, true));
        }

        [TestMethod]
        public void GetValueShouldNotThrowExceptionInvalidValueIfFlagFalse()
        {
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel();
            const string propertyName = "invalid";
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, false).ShouldEqual(null);
        }

        [TestMethod]
        public void GetValueShouldReturnValueUsingConverterSource()
        {
            bool converterInvoked = false;
            var memberMock = new BindingMemberInfoMock
            {
                Type = typeof(int)
            };
            CultureInfo culture = CultureInfo.InvariantCulture;
            var parameter = new object();
            var sourceModel = new BindingSourceModel();
            var converterMock = new ValueConverterCoreMock
            {
                Convert = (o, type, arg3, arg4) =>
                {
                    converterInvoked = true;
                    o.ShouldEqual(sourceModel.IntProperty);
                    type.ShouldEqual(typeof(int));
                    arg3.ShouldEqual(parameter);
                    arg4.ShouldEqual(culture);
                    return int.MaxValue;
                }
            };
            var dataContext = new DataContext
            {
                {BindingBuilderConstants.Converter, d => converterMock},
                {BindingBuilderConstants.ConverterCulture, d => culture},
                {BindingBuilderConstants.ConverterParameter, d => parameter}
            };

            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, dataContext, true);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(int.MaxValue);
            converterInvoked.ShouldBeTrue();
        }

        [TestMethod]
        public void GetValueShouldReturnValueUsingConverterTarget()
        {
            bool converterInvoked = false;
            var memberMock = new BindingMemberInfoMock
            {
                Type = typeof(int)
            };
            CultureInfo culture = CultureInfo.InvariantCulture;
            var parameter = new object();
            var sourceModel = new BindingSourceModel();
            var converterMock = new ValueConverterCoreMock
            {
                ConvertBack = (o, type, arg3, arg4) =>
                {
                    converterInvoked = true;
                    o.ShouldEqual(sourceModel.IntProperty);
                    type.ShouldEqual(typeof(int));
                    arg3.ShouldEqual(parameter);
                    arg4.ShouldEqual(culture);
                    return int.MaxValue;
                }
            };
            var dataContext = new DataContext
            {
                {BindingBuilderConstants.Converter, d => converterMock},
                {BindingBuilderConstants.ConverterCulture, d => culture},
                {BindingBuilderConstants.ConverterParameter, d => parameter}
            };

            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, dataContext, false);
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(int.MaxValue);
            converterInvoked.ShouldBeTrue();
        }

        [TestMethod]
        public void GetValueShouldReturnValueUsingTargetNullValueTarget()
        {
            const int targetNullValue = 0;
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel { IntProperty = targetNullValue };
            var dataContext = new DataContext
            {
                {BindingBuilderConstants.TargetNullValue, targetNullValue},                
            };

            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, dataContext, false);
            valueAccessor.GetValue(memberMock, dataContext, true).ShouldBeNull();
        }

        [TestMethod]
        public void GetValueShouldReturnValueUsingTargetNullValueSource()
        {
            const int targetNullValue = 0;
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel { ObjectProperty = null };
            var dataContext = new DataContext
            {
                {BindingBuilderConstants.TargetNullValue, targetNullValue},                
            };

            string propertyName = GetMemberPath<BindingSourceModel>(model => model.ObjectProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, dataContext, true);
            valueAccessor.GetValue(memberMock, dataContext, true).ShouldEqual(targetNullValue);
        }

        [TestMethod]
        public void GetValueShouldReturnValueUsingFallbackValueSource()
        {
            const int fallback = 0;
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel { ObjectProperty = BindingConstants.UnsetValue };
            var dataContext = new DataContext
            {
                {BindingBuilderConstants.Fallback, d => fallback},
            };

            string propertyName = GetMemberPath<BindingSourceModel>(model => model.ObjectProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, dataContext, true);
            valueAccessor.GetValue(memberMock, dataContext, true).ShouldEqual(fallback);
        }

        [TestMethod]
        public void SetValueShouldUpdateValueInSource()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return int.MaxValue;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.IntProperty.ShouldEqual(int.MaxValue);
        }

        [TestMethod]
        public void SetValueShouldUpdateValueInSourceIndexer()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model["test"]);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return propertyName;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel["test"].ShouldEqual(propertyName);
        }

        [TestMethod]
        public void SetValueShouldUpdateValueInSourceDoubleIndexer()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model["test", 0]);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return propertyName;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel["test", 0].ShouldEqual(propertyName);
        }

        [TestMethod]
        public void SetValueShouldUpdateValueInSourceNestedProperty()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel.IntProperty);
            var sourceModel = new BindingSourceModel { NestedModel = new BindingSourceNestedModel() };
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return int.MaxValue;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.NestedModel.IntProperty.ShouldEqual(int.MaxValue);
        }

        [TestMethod]
        public void SetValueShouldUpdateValueInSourceNestedPropertyIndexer()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test"]);
            var sourceModel = new BindingSourceModel { NestedModel = new BindingSourceNestedModel() };
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return propertyName;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.NestedModel["test"].ShouldEqual(propertyName);
        }

        [TestMethod]
        public void SetValueShouldUpdateValueInSourceNestedPropertyDoubleIndexer()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test", 0]);
            var sourceModel = new BindingSourceModel { NestedModel = new BindingSourceNestedModel() };
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return propertyName;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.NestedModel["test", 0].ShouldEqual(propertyName);
        }

        [TestMethod]
        public void SetValueShouldNotUpdateValueInSourceIfNestedPropertyNull()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel.IntProperty);
            var sourceModel = new BindingSourceModel();
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return int.MaxValue;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.NestedModel.ShouldBeNull();
        }

        [TestMethod]
        public void SetValueShouldNotUpdateValueInSourceIfNestedPropertyNullIndexer()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test"]);
            var sourceModel = new BindingSourceModel();
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return propertyName;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.NestedModel.ShouldBeNull();
        }

        [TestMethod]
        public void SetValueShouldNotUpdateValueInSourceIfNestedPropertyNullDoubleIndexer()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var propertyName = GetMemberPath<BindingSourceModel>(model => model.NestedModel["test", 0]);
            var sourceModel = new BindingSourceModel();
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return propertyName;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.NestedModel.ShouldBeNull();
        }

        [TestMethod]
        public void SetValueShouldDoNothingIfNewValueIsBindingDoNothing()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return BindingConstants.DoNothing;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.IntProperty.ShouldEqual(0);
        }


        [TestMethod]
        public void SetValueShouldDoNothingIfNewValueIsBindingUnsetValue()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return BindingConstants.UnsetValue;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.IntProperty.ShouldEqual(0);
        }


        [TestMethod]
        public void ValueChangingShouldBeRaised()
        {
            int oldValue = 0;
            int value = int.MaxValue;
            bool isInvoked = false;
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.ValueChanging += (sender, args) =>
            {
                sender.ShouldEqual(valueAccessor);
                args.OldValue.ShouldEqual(oldValue);
                args.NewValue.ShouldEqual(value);
                isInvoked = true;
            };

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return value;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            isInvoked.ShouldBeTrue();
            isInvoked = false;

            oldValue = value;
            value = int.MinValue;
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            isInvoked.ShouldBeTrue();
        }

        [TestMethod]
        public void ValueChangingShouldCanBeCanceled()
        {
            const int oldValue = 0;
            const int value = int.MaxValue;
            bool isInvoked = false;
            var srcAccessor = new BindingSourceAccessorMock();
            var memberMock = new BindingMemberInfoMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.ValueChanging += (sender, args) =>
            {
                sender.ShouldEqual(valueAccessor);
                args.OldValue.ShouldEqual(oldValue);
                args.NewValue.ShouldEqual(value);
                args.Cancel = true;
                isInvoked = true;
            };

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return value;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            isInvoked.ShouldBeTrue();
            valueAccessor.GetValue(memberMock, EmptyContext, true).ShouldEqual(oldValue);
        }

        [TestMethod]
        public void ValueChangedShouldBeRaised()
        {
            int oldValue = 0;
            int value = int.MaxValue;
            bool isInvoked = false;
            var sourceModel = new BindingSourceModel();
            var srcAccessor = new BindingSourceAccessorMock();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.ValueChanged += (sender, args) =>
            {
                sender.ShouldEqual(valueAccessor);
                args.OldValue.ShouldEqual(oldValue);
                args.NewValue.ShouldEqual(value);
                isInvoked = true;
            };

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return value;
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            isInvoked.ShouldBeTrue();
            isInvoked = false;

            oldValue = value;
            value = int.MinValue;
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            isInvoked.ShouldBeTrue();
        }

        [TestMethod]
        public void SetValueShouldThrowExceptionInvalidValueIfFlagTrue()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            srcAccessor.GetValue = (info, context, arg3) => string.Empty;
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            ShouldThrow(() => valueAccessor.SetValue(srcAccessor, EmptyContext, true));
        }

        [TestMethod]
        public void SetValueShouldNotThrowExceptionInvalidValueIfFlagFalse()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            srcAccessor.GetValue = (info, context, arg3) => string.Empty;
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.SetValue(srcAccessor, EmptyContext, false);
        }

        [TestMethod]
        public void SetValueShouldAutoConvertValueTrue()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.AutoConvertValue = true;

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return int.MaxValue.ToString();
            };
            valueAccessor.SetValue(srcAccessor, EmptyContext, true);
            sourceModel.IntProperty.ShouldEqual(int.MaxValue);
        }

        [TestMethod]
        public void SetValueShouldAutoConvertValueFalse()
        {
            var srcAccessor = new BindingSourceAccessorMock();
            var sourceModel = new BindingSourceModel();
            string propertyName = GetMemberPath<BindingSourceModel>(model => model.IntProperty);
            var valueAccessor = GetAccessor(sourceModel, propertyName, EmptyContext, true);
            valueAccessor.AutoConvertValue = false;

            srcAccessor.GetValue = (info, context, arg3) =>
            {
                context.ShouldEqual(EmptyContext);
                return int.MaxValue.ToString();
            };
            ShouldThrow<InvalidCastException>(() => valueAccessor.SetValue(srcAccessor, EmptyContext, true));
        }

        [TestMethod]
        public void GetEventValueShouldAlwaysReturnBindingMemberValue()
        {
            var memberMock = new BindingMemberInfoMock();
            var source = new BindingSourceModel();
            var accessor = GetAccessor(source, BindingSourceModel.EventName, EmptyContext, true);
            var memberValue = (BindingMemberValue)accessor.GetValue(memberMock, EmptyContext, true);
            memberValue.MemberSource.Target.ShouldEqual(source);
            memberValue.Member.MemberType.ShouldEqual(BindingMemberType.Event);
        }

        [TestMethod]
        public void SetValueShouldUpdateIsEnabledProperty()
        {
            bool canExecute = false;
            var command = new RelayCommand(o => { }, o => canExecute, this);

            var srcAccessor = new BindingSourceAccessorMock();
            var source = new BindingSourceModel();
            var accessor = GetAccessor(source, BindingSourceModel.EventName, EmptyContext, false);
            srcAccessor.GetValue = (info, context, arg3) => command;

            accessor.SetValue(srcAccessor, EmptyContext, true);
            source.IsEnabled.ShouldBeFalse();
            canExecute = true;
            command.RaiseCanExecuteChanged();
            source.IsEnabled.ShouldBeTrue();
        }

        [TestMethod]
        public void AccessorShouldUseCommandParameterCanExecute()
        {
            bool isInvoked = false;
            var parameter = new object();
            var command = new RelayCommand(o => { }, o =>
            {
                o.ShouldEqual(parameter);
                isInvoked = true;
                return false;
            }, this);
            var srcAccessor = new BindingSourceAccessorMock();
            var source = new BindingSourceModel();
            var accessor = GetAccessor(source, BindingSourceModel.EventName, EmptyContext, false);
            ((BindingTarget)accessor.Source).CommandParameterDelegate = d => parameter;
            srcAccessor.GetValue = (info, context, arg3) => command;

            accessor.SetValue(srcAccessor, EmptyContext, true);
            isInvoked.ShouldBeTrue();

            isInvoked = false;
            command.RaiseCanExecuteChanged();
            isInvoked.ShouldBeTrue();
        }

        [TestMethod]
        public void AccessorShouldUseOnlyOneCmd()
        {
            bool isEnabled = false;
            var oldCmd = new RelayCommand(o => { }, o => false, this);
            var command = new RelayCommand(o => { }, o => isEnabled, this);
            var srcAccessor = new BindingSourceAccessorMock();
            var source = new BindingSourceModel();
            var accessor = GetAccessor(source, BindingSourceModel.EventName, EmptyContext, false);
            srcAccessor.GetValue = (info, context, arg3) => oldCmd;

            accessor.SetValue(srcAccessor, EmptyContext, true);
            source.IsEnabled.ShouldBeFalse();
            oldCmd.RaiseCanExecuteChanged();
            source.IsEnabled.ShouldBeFalse();

            srcAccessor.GetValue = (info, context, arg3) => command;
            accessor.SetValue(srcAccessor, EmptyContext, true);
            isEnabled = true;
            command.RaiseCanExecuteChanged();
            source.IsEnabled.ShouldEqual(isEnabled);

            oldCmd.RaiseCanExecuteChanged();
            source.IsEnabled.ShouldEqual(isEnabled);

            srcAccessor.GetValue = (info, context, arg3) => null;
            accessor.SetValue(srcAccessor, EmptyContext, true);
            isEnabled = false;
            command.RaiseCanExecuteChanged();
            source.IsEnabled.ShouldBeTrue();
        }

        [TestMethod]
        public void ExecuteShouldCallCmdExecuteMethod()
        {
            var parameter = new object();
            bool isInvoked = false;
            var command = new RelayCommand(o =>
            {
                o.ShouldEqual(parameter);
                isInvoked = true;
            });
            var srcAccessor = new BindingSourceAccessorMock();
            var source = new BindingSourceModel();
            var accessor = GetAccessor(source, BindingSourceModel.EventName, EmptyContext, false);
            ((BindingTarget)accessor.Source).CommandParameterDelegate = d => parameter;
            srcAccessor.GetValue = (info, context, arg3) => command;
            accessor.SetValue(srcAccessor, EmptyContext, true);
            source.RaiseEvent();
            isInvoked.ShouldBeTrue();

            isInvoked = false;
            source.RaiseEvent();
            isInvoked.ShouldBeTrue();
        }

        protected virtual ISingleBindingSourceAccessor GetAccessor(object model, string path, IDataContext context, bool isSource)
        {
            var observer = new MultiPathObserver(model, BindingPath.Create(path), false);
            var source = isSource
                ? new BindingSource(observer)
                : new BindingTarget(observer);
            return new BindingSourceAccessor(source, context, !isSource);
        }

        #endregion

        #region Overrides of BindingTestBase

        protected override void OnInit()
        {
            base.OnInit();
            BindingServiceProvider.MemberProvider.Register(typeof(BindingSourceModel),
                    AttachedBindingMember.CreateMember<BindingSourceModel, bool>(AttachedMemberConstants.Enabled,
                        (info, sourceModel) => sourceModel.IsEnabled,
                        (info, sourceModel, value) => sourceModel.IsEnabled = value), true);
        }

        #endregion
    }
}