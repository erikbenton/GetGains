using GetGains.Core.Enums;

namespace GetGains.Core.Extensions
{
    public static class BodyPartExtensions
    {
        /// <summary>
        /// Gets the properly formatted body part label.
        /// </summary>
        /// <param name="bodyPart"></param>
        /// <returns>Body part string label.</returns>
        public static string GetLabel(this BodyPart bodyPart)
        {
            return bodyPart switch
            {
                BodyPart.Chest => "Chest",
                BodyPart.Legs => "Legs",
                BodyPart.UpperBack => "Upper Back",
                BodyPart.LowerBack => "Lower Back",
                BodyPart.Shoulders => "Shoulders",
                BodyPart.Biceps => "Biceps",
                BodyPart.Triceps => "Triceps",
                BodyPart.Forearms => "Forearms",
                BodyPart.Core => "Core",
                BodyPart.Other => "Other",
                _ => "N/A",
            };
        }
    }
}
