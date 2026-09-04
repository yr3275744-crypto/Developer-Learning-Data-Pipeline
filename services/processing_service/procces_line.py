import pandas as pd
from pathlib import Path

def experienceLevel(yearsCode):
    if pd.isna(yearsCode):
        return "Unknown"
    if 0 <= yearsCode <= 2:
        return "Beginner"
    if 3 <= yearsCode <= 5:
        return "Early Career"
    if 6 <= yearsCode <= 10:
        return "Experienced"
    if 11 <= yearsCode:
        return "Highly Experienced"
    else:
        return "Unknown"

# basePath = Path(__file__).resolve().parent[2]
def procces(raw:dict) -> dict:
    df = pd.DataFrame(raw, index= [0])
    df["YearsCode"] =pd.to_numeric(df["YearsCode"],errors="coerce").astype("Int64")
    df["ResponseId"] =pd.to_numeric(df["ResponseId"],errors="coerce").astype("int64")

    # %%
    pd.set_option("display.max_colwidth", 50)
    df["LearnCode"] = df["LearnCode"].str.split(";")
    df["AILearnHow"] = df["AILearnHow"].str.split(";")

    df["experienceLevel"] = df["YearsCode"].apply(experienceLevel)
    df["experienceLevel"] = df["YearsCode"].apply(experienceLevel)



    # %%
    TECH_DOC = "Technical documentation (is generated for/by the tool or system)"
    AI_CODEGEN = "AI CodeGen tools or AI-enabled apps"
    STACK_OVERFLOW = "Stack Overflow or Stack Exchange"
    def has_options(content, target):
        if type(content)!= list:
            return False
        return (target in content)

    df["usesDocumentation"] = df["LearnCode"].apply(lambda x: has_options(x, TECH_DOC))
    df['usesAIForLearning'] = df['LearnCode'].apply(lambda x: has_options(x, AI_CODEGEN))
    df['usesStackOverflow'] = df['LearnCode'].apply(lambda x: has_options(x, STACK_OVERFLOW))

    # %%
    df = df.rename(columns={
        'ResponseId': 'responseId',
        'Age': 'age',
        'YearsCode': 'yearsCode',
        'DevType': 'devType',
        'LearnCodeChoose': 'learnCodeChoose',
        'LearnCode': 'learningMethods',
        'LearnCodeAI': 'learnCodeAI',
        'AILearnHow': 'aiLearningMethods',
        'AISelect': 'aiUsage',
        'AIAcc': 'aiTrust',
        'AISent': 'aiSentiment',
    })

    # %%
    # df.to_json(basePath / "data/developer_ai_learning_summery.json", orient="records", lines=True)
    return df.to_dict(orient="records")
