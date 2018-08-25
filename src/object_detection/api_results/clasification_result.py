import json
import jsonpickle

class ClassificationResultRow(json.JSONEncoder):
    score: None
    position: None
    category: None
    
    def __init__(self, score, position, category):
        self.score = score
        self.position = position
        self.category = category

    def default(self, o):
        if isinstance(o, ClassificationResultRow):
            return {'result': o.__dict__}

        return {'__{}__'.format(o.__class__.__name__): o.__dict__}

class ClassificationResult:
    path: None
    result = []
    id: None

    def __init__(self, path, max_results, scores, classes, category_index, positions):
        for i in range(0, max_results):
            if scores[i] < 0.5:
                continue
            box = tuple(positions[i].tolist())
            category = category_index[classes[i]]['name']
            self.result.append(ClassificationResultRow(scores[i].item(), box, category))
        self.path = path

    def set_id(self, id):
        self.id = id

    def to_message(self):
        # result_list_json = json.dumps(, default=self)
        result_dict = {"result": [ob.__dict__ for ob in self.result], "path": self.path, "id": self.id}
        result_json = json.dumps(result_dict)
        return result_json


