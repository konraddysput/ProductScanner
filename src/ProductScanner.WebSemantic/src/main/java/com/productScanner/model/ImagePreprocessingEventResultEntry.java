package com.productScanner.model;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ImagePreprocessingEventResultEntry {
    public int Id;
    public Map<String,String> Data;
    public List<String> Types;

    public  ImagePreprocessingEventResultEntry(){
        Data = new HashMap<>();
        Types= new ArrayList<>();
    }

}
