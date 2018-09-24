import json


class ClassificationResultRow:
    score: None
    position: None
    category: None

    def __init__(self, score, position, category):
        self.score = score
        self.position = position
        self.category = category


class ClassificationResult:

    def __init__(self, path, max_results, scores, classes, category_index, positions, source_path):
        self.result = []
        for i in range(0, max_results):
            if scores[i] < 0.5:
                continue
            box = tuple(positions[i].tolist())
            category = category_index[classes[i]]['name']
            self.result.append(ClassificationResultRow(scores[i].item(), box, category))
        self.path = path
        self.source_path = source_path

    def set_id(self, id):
        self.id = id

    def to_message(self):
        result_dict = {"data": [ob.__dict__ for ob in self.result], "analysedFilePath": self.path,
                       "path": self.source_path, "id": self.id}
        result_json = json.dumps(result_dict)
        print('output json: ' + result_json)
        return result_json
