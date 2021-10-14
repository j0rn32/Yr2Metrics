// ReSharper disable InconsistentNaming
public record NowCastObservation
{
    public Property properties { get; init; }

    public record Property
    {
        public Meta meta { get; set; }
        public TimeSeries[] timeseries { get; init; }

        public record Meta
        {
            public DateTime updated_at { get; init; }
            public string radar_coverage { get; init; }
        }

        public record TimeSeries
        {
            public Data data { get; init; }

            public record Data
            {
                public Instant instant { get; init; }

                public record Instant
                {
                    public Details details { get; init; }

                    public record Details
                    {
                        public float? air_temperature { get; init; }
                        public float? precipitation_rate { get; init; }
                        public float? relative_humidity { get; init; }
                        public float? wind_from_direction { get; init; }
                        public float? wind_speed { get; init; }
                        public float? wind_speed_of_gust { get; init; }
                    }
                }
            }
        }
    }
}