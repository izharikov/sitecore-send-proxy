using System.Diagnostics.CodeAnalysis;

namespace SitecoreSendProxy.Models.Track
{
    [SuppressMessage("ReSharper", "InconsistentNaming")] 
    public enum ActionType
    {
        IDENTIFY,
        ADDED_TO_ORDER,
        ORDER_COMPLETED,
        PAGE_VIEWED,
        EXIT_INTENT,
    }
}