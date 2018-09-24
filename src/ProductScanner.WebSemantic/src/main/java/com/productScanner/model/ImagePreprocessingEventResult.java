package com.productScanner.model;

import java.util.ArrayList;
import java.util.List;

public class ImagePreprocessingEventResult {
    public int Id;
    public List<ImagePreprocessingEventResultEntry> Data;

    public  ImagePreprocessingEventResult(){
        Data = new ArrayList<>();
    }
}
