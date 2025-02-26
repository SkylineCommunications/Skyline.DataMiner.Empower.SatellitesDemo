using System;
using System.Threading;

namespace Satellites
{
    /// <summary>
    /// Represents a trigger that can periodically execute some logic.
    /// </summary>
    public sealed class Trigger : IDisposable
    {
        private static readonly TimeSpan _interval = TimeSpan.FromSeconds(1);

        private readonly Action _handler;
        private readonly Timer _timer;

        /// <summary>
        /// Creates a new trigger that periodically calls the specified <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">The logic that should be executed.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="handler"/> is null.</exception>
        public Trigger(Action handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _timer = new Timer(DoTrigger, null, _interval, _interval);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="Trigger"/>.
        /// </summary>
        public void Dispose()
        {
            _timer.Dispose();
        }

        private void DoTrigger(object state)
        {
            try
            {
                _handler.Invoke();
            }
            catch
            {
                // Ignore exceptions
            }
        }
    }
}
