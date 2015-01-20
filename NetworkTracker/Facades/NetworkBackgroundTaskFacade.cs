using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using NetworkTracker.BackgroundCore;

namespace NetworkTracker.WindowsPhone.Facades
{
    /// <summary>
    /// Facade for background network task configuration
    /// </summary>
    public sealed class NetworkBackgroundTaskFacade
    {
        const string TaskName = "NetworkBackgroundTask";

        /// <summary>
        /// Registers the background network task.
        /// </summary>
        /// <returns>Regisred background task</returns>
        public async Task<BackgroundTaskRegistration> RegisterBackgroundTask()
        {
            BackgroundTaskRegistration taskRegistration = null;

            if (await hasAccessAsync())
            {
                var builder = new BackgroundTaskBuilder { Name = TaskName, TaskEntryPoint = typeof(NetworkBackgroundTask).FullName };
                var trigger = new SystemTrigger(SystemTriggerType.NetworkStateChange, false);
                builder.SetTrigger(trigger);

                taskRegistration = builder.Register();
            }

            return taskRegistration;
        }

        /// <summary>
        /// Unregisters the background network task.
        /// </summary>
        public void UnregisterBackgroundTask()
        {
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == TaskName)
                {
                    cur.Value.Unregister(true);
                }
            }
        }

        /// <summary>
        /// Determines whether is network task registered
        /// </summary>
        /// <param name="taskRegistration">The registered previously network task.</param>
        /// <returns>Value indicating if task was registred</returns>
        public bool IsTaskRegistered(out BackgroundTaskRegistration taskRegistration)
        {
            taskRegistration = null;

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == TaskName)
                {
                    taskRegistration = (BackgroundTaskRegistration) task.Value;
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> hasAccessAsync()
        {
            var result = await BackgroundExecutionManager.RequestAccessAsync();
            return result != BackgroundAccessStatus.Denied && result != BackgroundAccessStatus.Unspecified;
        }
    }

}
