# %%
import pandas as pd
from pathlib import Path

# basePath = Path(__file__).resolve().parent[2]
basePath = Path.cwd().resolve().parent

df = pd.read_csv(basePath / "data/developer_ai_learning_raw.csv")
print(df.info())

# %%
# df["YearsCode"] = df["ResponseId"].astype(int, errors="ignore")
# df["YearsCode"] = df["YearsCode"].astype("int64", errors="ignore")
df["YearsCode"] =df["YearsCode"].astype("Int64")

# %%
print(df.info())
print(df.isna().sum())

# %%
pd.set_option("display.max_colwidth", 50)
df["LearnCode"] = df["LearnCode"].str.split(";")
df["AILearnHow"] = df["AILearnHow"].str.split(";")
print(df.head(3))
print(df.info())

# %%
# pd.set_option("display.max_colwidth", 50)
# print(df.info())
print(df["LearnCode"].dtype)


# %%
# df.to_json(basePath / "data/developer_ai_learning_clean.json", orient="records", lines=True)
df.to_csv(basePath / "data/developer_ai_learning_clean.csv", index=False)

# %%
counts = df['LearnCode'].value_counts()
mx = counts.max()
print(
    counts[counts == mx].index[0]
)

# %%
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
print(df.info())

# %%
df.to_json(basePath / "data/developer_ai_learning_summery.json", orient="records", lines=True)


