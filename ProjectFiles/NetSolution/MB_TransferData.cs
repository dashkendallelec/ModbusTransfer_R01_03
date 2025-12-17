#region Using directives
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using FTOptix.Modbus;
using FTOptix.NativeUI;
using FTOptix.NetLogic;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.System;
using FTOptix.UI;
using FTOptix.WebUI;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
#endregion

public class MB_TransferData : BaseNetLogic
{
    public override void Start()
    {
        //Setup the Remote Variable Synchronizer
        SetupSynchronizer();

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        synchronizer.Dispose();
    }

    private void SetupSynchronizer()
    {
        //Assign the Modbus variables and the output variables (packed arrays)
        modbusReals = Project.Current.GetVariable("CommDrivers/ModbusDriver1/ModbusStation1/Tags/Read_Reals");
        modbusDints = Project.Current.GetVariable("CommDrivers/ModbusDriver1/ModbusStation1/Tags/Read_Dints");

        //Setup the Remote Variable Synchronizer to handle the Modbus communication and transfer to ControlLogix
        synchronizer = new RemoteVariableSynchronizer();
        synchronizer.Add(new RemoteVariable(modbusReals, 9));
        synchronizer.Add(new RemoteVariable(modbusDints, 9));


    }

    //Modbus driver tags
    private IUAVariable modbusReals;
    private IUAVariable modbusDints;

    //Remote Variable Synchronizer to start Modbus communications
    //and optimize data transfer for Logix Driver
    RemoteVariableSynchronizer synchronizer;

}
