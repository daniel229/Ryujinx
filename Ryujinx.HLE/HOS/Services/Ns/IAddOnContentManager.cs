using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Ns
{
    [Service("aoc:u")]
    class IAddOnContentManager : IpcService
    {
        public IAddOnContentManager(ServiceCtx context) { }

        [Command(2)]
        // CountAddOnContent(u64, pid) -> u32
        public static ResultCode CountAddOnContent(ServiceCtx context)
        {
            context.ResponseData.Write(context.Device.System.ContentManager.GetCurrentApplicationAocDataCount());

            return ResultCode.Success;
        }

        [Command(3)]
        // ListAddOnContent(u32, u32, u64, pid) -> (u32, buffer<u32, 6>)
        public static ResultCode ListAddOnContent(ServiceCtx context)
        {
            int[] aocIndices = context.Device.System.ContentManager.GetCurrentApplicationAocDataIndices();

            for (int index = 0; index < aocIndices.Length; index++)
            {
                long address = context.Request.ReceiveBuff[0].Position + index * 4;
                context.Memory.WriteInt32(address, aocIndices[index]);
            }

            context.ResponseData.Write(aocIndices.Length);

            return ResultCode.Success;
        }

        [Command(5)]
        public static ResultCode GetAddOnContentBaseId(ServiceCtx context)
        {
            context.ResponseData.Write(context.Process.TitleId + 0x1000);

            return ResultCode.Success;
        }

        [Command(7)]
        public static ResultCode PrepareAddOnContent(ServiceCtx context)
        {
            Logger.PrintStub(LogClass.ServiceNs);

            return ResultCode.Success;
        }
    }
}