using Python.Runtime;

namespace Application.Helpers.MachineLearningModel
{
    public static class LightGBMPredictor
    {
        private static bool _initialized = false;

        public static List<PredictionResult> Predict(
            List<(int id, float proximityKm, float avgRating)> inputs
        )
        {
            if (!_initialized)
            {
                Runtime.PythonDLL = @"C:\Program Files\Python313\python313.dll";

                PythonEngine.Initialize();
                _initialized = true;
            }

            using (Py.GIL())
            {
                //dynamic sys = Py.Import("sys");

                //sys.path.append(
                //    $"D:\\Projects\\TechnoSewa\\Application\\Helpers\\MachineLearningModel"
                //);
                var scriptPath =
                    $"D:\\Projects\\TechnoSewa\\Application\\Helpers\\MachineLearningModel";
                dynamic sys = Py.Import("sys");
                sys.path.append(Path.Combine(scriptPath));

                dynamic predictor = Py.Import("predictor");

                var pyInput = new PyList();
                foreach (var (id, proximityKm, avgRating) in inputs)
                {
                    var dict = new PyDict();
                    dict["Id"] = new PyInt(id);
                    dict["Proximity (km)"] = new PyFloat(proximityKm);
                    dict["AvgRating"] = new PyFloat(avgRating);
                    pyInput.Append(dict);
                }

                dynamic result = predictor.predict(pyInput);

                var output = new List<PredictionResult>();
                foreach (dynamic item in result)
                {
                    var id = (int)item["Id"];
                    var score = (float)item["Score"];
                    output.Add(new PredictionResult { Id = id, Score = score });
                }

                return output;
            }
        }
    }
}
