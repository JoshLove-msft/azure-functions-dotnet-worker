// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Converters;
using Microsoft.Azure.Functions.Worker.Tests.Converters;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Microsoft.Azure.Functions.WorkerExtension.Tests
{
    public class QueueMessageConverterTests
    {
        public QueueMessageConverterTests()
        {
            var host = new HostBuilder().ConfigureFunctionsWorkerDefaults((WorkerOptions options) => { }).Build();
        }

        [Fact]
        public async Task ConvertAsync_ValidModelBindingData_QueueMessage_ReturnsSuccess()
        {
            var grpcModelBindingData = QueuesTestHelper.GetTestGrpcModelBindingData();
            var context = new TestConverterContext(typeof(QueueMessage), grpcModelBindingData);

            var queueMessageConverter = new QueueMessageConverter();
            var conversionResult = await queueMessageConverter.ConvertAsync(context);

            var expectedData = conversionResult.Value as QueueMessage;
            Assert.Equal(ConversionStatus.Succeeded, conversionResult.Status);
            Assert.Equal("hello world", expectedData.Body.ToString());
        }

        [Fact]
        public async Task ConvertAsync_ContentSource_AsObject_ReturnsUnhandled()
        {
            var context = new TestConverterContext(typeof(QueueMessage), new Object());

            var queueMessageConverter = new QueueMessageConverter();
            var conversionResult = await queueMessageConverter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Unhandled, conversionResult.Status);
        }

        [Fact]
        public async Task ConvertAsync_ModelBindingData_Null_ReturnsUnhandled()
        {
            var context = new TestConverterContext(typeof(QueueMessage), null);

            var queueMessageConverter = new QueueMessageConverter();
            var conversionResult = await queueMessageConverter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Unhandled, conversionResult.Status);
        }

        [Fact]
        public async Task ConvertAsync_ModelBindingDataSource_NotQueueStorageExtension_ReturnsUnhandled()
        {
            var grpcModelBindingData = QueuesTestHelper.GetTestGrpcModelBindingData(source: "anotherExtensions");
            var context = new TestConverterContext(typeof(QueueMessage), grpcModelBindingData);

            var queueMessageConverter = new QueueMessageConverter();
            var conversionResult = await queueMessageConverter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Unhandled, conversionResult.Status);
        }

        [Fact]
        public async Task ConvertAsync_ModelBindingDataContentType_Unsupported_ReturnsFailed()
        {
            var grpcModelBindingData = QueuesTestHelper.GetTestGrpcModelBindingData(contentType: "binary");
            var context = new TestConverterContext(typeof(QueueMessage), grpcModelBindingData);

            var queueMessageConverter = new QueueMessageConverter();
            var conversionResult = await queueMessageConverter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Failed, conversionResult.Status);
            Assert.Equal("Unexpected content-type. Currently only 'application/json' is supported.", conversionResult.Error.Message);
        }
    }
}
