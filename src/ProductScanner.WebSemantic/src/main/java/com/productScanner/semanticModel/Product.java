package com.productScanner.semanticModel;

import org.apache.jena.rdf.model.*;
import org.apache.jena.util.FileManager;
import org.apache.jena.vocabulary.VCARD;

import java.io.*;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class Product extends ModelBase {

    public  Product(int id, String classifier, float width, float height, List<ProductRule> rules){
        super("http://productScanner/product/", classifier);
        this.id= id;
        this.width = width;
        this.height = height;
        this.rules = rules;

    }
    public List<ProductRule>rules;
    public int id;
    public float width; //milimeter -51
    public float height; //milimeter - 141

    private Model _model;

}