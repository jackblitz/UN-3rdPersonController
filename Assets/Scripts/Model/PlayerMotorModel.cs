using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.U2D;

public class PlayerMotorModel : BaseMotorModel
{
    public bool IsAttached { get; set; }

    public bool IsAiming { get; set; }

    public string GetConsoleLog()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(base.GetConsoleLog());
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Attacking: ");
        stringBuilder.Append(IsAttached);
        stringBuilder.AppendLine();
        stringBuilder.Append("Is Aiming: ");
        stringBuilder.Append(IsAiming);
        return stringBuilder.ToString();
    }
}
