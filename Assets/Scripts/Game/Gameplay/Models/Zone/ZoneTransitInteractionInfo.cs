namespace Game.Gameplay.Models.Zone 
{
    public class ZoneTransitInteractionInfo
    {
        public bool IsAllowedToProcessOpening { get; private set; }
        public ZoneTransitEssenceData[] CurrentEssenceDataset { get; private set; }
        public float ElapsedTime { get; private set; }

        public float MaxElapsedTime { get; private set; } = 1f;
        public  float OpeningSpeed { get; private set; } = 2f;

        public void AllowOpeningProcess() 
        {
            IsAllowedToProcessOpening = true;
        }

        public void DisallowOpeningProcess() 
        {
            IsAllowedToProcessOpening = false;
        }

        public void SetCurrentEssencesDataset(ZoneTransitEssenceData[] essenceDataset) 
        {
            CurrentEssenceDataset = essenceDataset;
        }

        public void ChangeElapsedTime(float time) 
        {
            ElapsedTime = time;
        }
    }
}