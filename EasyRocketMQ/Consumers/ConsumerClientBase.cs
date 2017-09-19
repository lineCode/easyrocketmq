﻿namespace EasyRocketMQ.Consumers
{
    /// <summary>
    /// 消息者基类
    /// </summary>
    public abstract class ConsumerClientBase : RocketClientBase
    {
        /// <summary>
        /// 消费者Id
        /// </summary>
        protected string ConsumerId { get; private set; }

        /// <summary>
        /// 主题
        /// </summary>
        protected string Topic { get; private set; }

        /// <summary>
        /// 子表达式
        /// </summary>
        protected string SubExpression { get; private set; }

        /// <summary>
        /// 消息者线程数
        /// </summary>
        protected int ConsumerThreadCount { get; set; } = 10;

        public ConsumerClientBase(string accessKeyId, string accessKeySecret, string topic, string consumerId,
                                    string subExpression = "*", int consumerThreadCount = 10)
            : base(accessKeyId, accessKeySecret)
        {
            this.ConsumerId = consumerId;
            this.Topic = topic;
            this.SubExpression = subExpression;
            this.ConsumerThreadCount = consumerThreadCount;
        }
    }
}