package com.productScanner.semanticModel;

import org.apache.commons.lang3.StringUtils;

public class ProductRule extends ModelBase {

    public String source;
    public String compared;
    public float value;
    public RuleType ruleType;

    public  ProductRule(String ruleName, String source, RuleType ruleType){
        this(ruleName, source, ruleType, StringUtils.EMPTY, 0);

    }
    public ProductRule(String ruleName, String source,  RuleType ruleType, String compared, float value){
        super("http://productScanner/productrule/", ruleName);
        this.source = source;
        this.compared = compared;
        this.value = value;
        this.ruleType = ruleType;
    }
}
