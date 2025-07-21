import lightgbm as lgb
import pandas as pd
import os

# Load the LightGBM model
model_path = r"D:\Projects\TechnoSewa\Application\Helpers\MachineLearningModel\lgbm_model1.txt"

if not os.path.exists(model_path):
    raise FileNotFoundError(f"Model file not found at: {model_path}")

model = lgb.Booster(model_file=model_path)

def predict(data):
    if not data:
        return []

    df_full = pd.DataFrame(data)

    required_cols = ["Id", "Proximity (km)", "AvgRating"]
    if not all(col in df_full.columns for col in required_cols):
        return []

    ids = df_full["Id"].tolist()
    features = df_full[["Proximity (km)", "AvgRating"]]

    preds = model.predict(features, group=[len(features)])

    # Combine and sort results by Score (ascending)
    results = [{"Id": id_val, "Score": float(score)} for id_val, score in zip(ids, preds)]
    results.sort(key=lambda x: x["Score"])

    return results
