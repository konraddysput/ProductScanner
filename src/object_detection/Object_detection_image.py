import os
import uuid
import cv2
import numpy as np
import tensorflow as tf
import sys
import scipy
import scipy.misc

# This is needed since the notebook is stored in the object_detection folder.
from api_results.clasification_result import ClassificationResult

sys.path.append("..")

# Import utilites
from utils import label_map_util
from utils import visualization_utils as vis_util


class ObjectDetection:
    # Name of the directory containing the object detection module we're using
    MODEL_NAME = 'inference_graph'
    NUM_CLASSES = 3

    def __init__(self):
        # Grab path to current working directory
        cwd_path = os.getcwd()

        # Path to frozen detection graph .pb file, which contains the model that is used
        # for object detection.
        path_to_detection_graph = os.path.join(cwd_path, self.MODEL_NAME, 'frozen_inference_graph.pb')

        # Path to label map file
        path_to_labels = os.path.join(cwd_path, 'training', 'labelmap.pbtxt')

        # Load the label map.
        # Label maps map indices to category names, so that when our convolution
        # network predicts `5`, we know that this corresponds to `king`.
        # Here we use internal utility functions, but anything that returns a
        # dictionary mapping integers to appropriate string labels would be fine
        label_map = label_map_util.load_labelmap(path_to_labels)
        categories = label_map_util.convert_label_map_to_categories(
            label_map,
            max_num_classes=self.NUM_CLASSES,
            use_display_name=True)
        self.category_index = label_map_util.create_category_index(categories)

        # Load the Tensorflow model into memory.
        detection_graph = tf.Graph()
        with detection_graph.as_default():
            od_graph_def = tf.GraphDef()
            with tf.gfile.GFile(path_to_detection_graph, 'rb') as fid:
                serialized_graph = fid.read()
                od_graph_def.ParseFromString(serialized_graph)
                tf.import_graph_def(od_graph_def, name='')

            self.sess = tf.Session(graph=detection_graph)

        # Define input and output tensors (i.e. data) for the object detection classifier

        # Input tensor is the image
        self.image_tensor = detection_graph.get_tensor_by_name('image_tensor:0')

        # Output tensors are the detection boxes, scores, and classes
        # Each box represents a part of the image where a particular object was detected
        self.detection_boxes = detection_graph.get_tensor_by_name('detection_boxes:0')

        # Each score represents level of confidence for each of the objects.
        # The score is shown on the result image, together with the class label.
        self.detection_scores = detection_graph.get_tensor_by_name('detection_scores:0')
        self.detection_classes = detection_graph.get_tensor_by_name('detection_classes:0')

        # Number of objects detected
        self.num_detections = detection_graph.get_tensor_by_name('num_detections:0')

    def analyse(self, path_to_image, show_image):
        # Load image using OpenCV and
        # expand image dimensions to have shape: [1, None, None, 3]
        # i.e. a single-column array, where each item in the column has the pixel RGB value
        image = cv2.imread(path_to_image)
        image_expanded = np.expand_dims(image, axis=0)

        # Perform the actual detection by running the model with the image as input
        (boxes, scores, classes, num) = self.sess.run(
            [self.detection_boxes, self.detection_scores, self.detection_classes, self.num_detections],
            feed_dict={self.image_tensor: image_expanded})
        # maximum number of boxes to draw on image
        # maximum number of results we expect on our image
        # in our experiment we don't need to provide more boxes/objects
        max_boxes_to_draw = 10
        vis_util.visualize_boxes_and_labels_on_image_array(
            image,
            np.squeeze(boxes),
            np.squeeze(classes).astype(np.int32),
            np.squeeze(scores),
            self.category_index,
            max_boxes_to_draw=max_boxes_to_draw,
            use_normalized_coordinates=True,
            line_thickness=8,
            min_score_thresh=0.80)

        if show_image:
            cv2.imshow('Object detector', image)
            cv2.waitKey(0)
            cv2.destroyAllWindows()
            return None
        else:
            return ClassificationResult(
                path=self.save(path_to_image, image),
                max_results=max_boxes_to_draw,
                scores=np.squeeze(scores),
                classes=np.squeeze(classes).astype(np.int32),
                category_index=self.category_index,
                positions=np.squeeze(boxes),
                source_path=path_to_image)

    def save(self, path_to_image, image):
        save_path = self.get_save_path(path_to_image)
        image_file = open(save_path, 'x')
        scipy.misc.imsave(image_file, image)
        return save_path

    def get_save_path(self, path_to_image):
        root_img_path = os.path.dirname(path_to_image)
        file_extension = os.path.splitext(path_to_image)[1]
        file_name = 'p-' + str(uuid.uuid4()) + file_extension
        return os.path.join(root_img_path, file_name)


def main():
    o = ObjectDetection()
    current_directory = os.getcwd()
    path_to_image = os.path.join(current_directory, 'test_images', 'test4.jpg')
    o.analyse(path_to_image, True)


if __name__ == "__main__":
    main()
