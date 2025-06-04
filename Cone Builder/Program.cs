// Cone Builder
using System;
using NXOpen;
using NXOpen.Mechatronics;

public class ConeBuilder
{
    public static void Main(string[] args)
    {
        try
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;

            if (workPart == null)
            {
                Console.WriteLine("No active work part found.");
                return;
            }

            var cone = CreateCone(workPart, 50.0, 30.0, 100.0);
            Console.WriteLine("Cone created successfully: " + (cone != null));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    /// <summary>
    /// Creates a cone feature in the given NX part.
    /// </summary>
    /// <param name="workPart">The NX part to add the cone to.</param>
    /// <param name="baseDiameter">The diameter of the base of the cone.</param>
    /// <param name="topDiameter">The diameter of the top of the cone.</param>
    /// <param name="height">The height of the cone.</param>
    /// <returns>The created Cone feature.</returns>
    public static NXOpen.Features.Cone CreateCone(NXOpen.Part workPart, double baseDiameter, double topDiameter, double height)
    {
      
        var coneBuilder = workPart.Features.CreateConeBuilder(null);
        coneBuilder.Type = NXOpen.Features.ConeBuilder.Types.DiametersAndHeight;
        coneBuilder.BaseDiameter.RightHandSide = baseDiameter.ToString();
        coneBuilder.TopDiameter.RightHandSide = topDiameter.ToString();
        coneBuilder.Height.RightHandSide = height.ToString();
        coneBuilder.BooleanOption.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;

        NXOpen.Features.Cone coneFeature = null;
        try
        {
            coneFeature = (NXOpen.Features.Cone)coneBuilder.Commit();
        }
        finally
        {
            coneBuilder.Destroy();
        }
        return coneFeature;
    }
}
