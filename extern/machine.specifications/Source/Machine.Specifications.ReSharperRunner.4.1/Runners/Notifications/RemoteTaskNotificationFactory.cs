﻿using JetBrains.ReSharper.TaskRunnerFramework;

using Machine.Specifications.ReSharperRunner.Tasks;

namespace Machine.Specifications.ReSharperRunner.Runners.Notifications
{
  internal class RemoteTaskNotificationFactory
  {
    public RemoteTaskNotification CreateTaskNotification(TaskExecutionNode node)
    {
      var remoteTask = node.RemoteTask;

      if (remoteTask is ContextSpecificationTask)
      {
        return new ContextSpecificationRemoteTaskNotification(node);
      }

      if (remoteTask is BehaviorSpecificationTask)
      {
        return new BehaviorSpecificationRemoteTaskNotification(node);
      }

      return new SilentRemoteTaskNotification();
    }
  }
}