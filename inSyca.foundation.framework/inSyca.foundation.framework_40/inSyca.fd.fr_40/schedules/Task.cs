using inSyca.foundation.framework.configuration;

namespace inSyca.foundation.framework.schedules
{
	public class Task
	{
		#region Attributes

		private string m_name;
		/// <summary>
		/// The name describing the task to
		/// be executed
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		private DTaskStart m_startCallback = null;
		/// <summary>
		/// The callback to execute when
		/// starting this task
		/// </summary>
		public DTaskStart StartCallback
		{
			get
			{
				return m_startCallback;
			}
			set
			{
				m_startCallback = value;
			}
		}

		private AppSchedulesInstanceElement m_appSchedule = null;
		/// <summary>
		/// The callback to execute when
		/// starting this task
		/// </summary>
		public AppSchedulesInstanceElement AppSchedule
		{
			get
			{
				return m_appSchedule;
			}
			set
			{
				m_appSchedule = value;
			}
		}

		#endregion

		public Task(AppSchedulesInstanceElement appSchedule, DTaskStart startCallback)
		{
			this.Name = appSchedule.Name;
			this.StartCallback = startCallback;
			this.AppSchedule = appSchedule;
		}

		public Task(AppSchedulesInstanceElement appSchedule)
		{
			this.Name = appSchedule.Name;
			this.AppSchedule = appSchedule;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The name of the task. Don't give tasks
		/// the same name!</param>
		/// <param name="startCallback">The callback to be executed on starttime is reached</param>
		public Task(string name, DTaskStart startCallback)
		{
			this.Name = name;
			this.StartCallback = startCallback;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The name of the task. Don't give tasks
		/// the same name!</param>
		public Task(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="rhs"></param>
		public Task(Task rhs)
		{
			this.m_name = rhs.m_name;
			this.m_startCallback = rhs.m_startCallback;
		}

		public override string ToString()
		{
			return string.Format("inSyca.foundation.framework.schedules.Task (Name: {0}, OccurrenceStartTime: {1}, OccurrenceEndTime: {2}, Occurrence: {3})", Name, AppSchedule.OccurrenceStartTime, AppSchedule.OccurrenceEndTime, AppSchedule.Occurrence);
		}
	}

	public delegate void DTaskStart(Task _task);

}
