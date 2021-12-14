% Popular Machine Learning Models for Classification
% Support Vector Machine (SVM) category
% https://www.mathworks.com/discovery/machine-learning-models.html

% 1 preparing dataset, dividing it train/test set - (features (columns) samples/records/id's/patients(rows) responses (cols + rows), targets/predictors/classes/labels/outputs for division and classification)
% 2 preparing validation set out of training set (kfold cv)
% 3 Feature selection
% 4 fiding best parameters (hyper)
% 5 test the model with test set
% 6 visualize hyperplane

clear; close all; clc; % if you want to "re-train" comment out this line

%% preparing dataset

load wdbc.mat

targets_num = t; % predictors
%%

% binary classification 569 samples to make into shape...
%X = zeros(569,30);
X = x; % 569 samples (rows) 30 features (columns)
y = targets_num(1:569);

%%
% 80:20
rand_num = randperm(size(X,1)); % 569 samples
X_train = X(rand_num(1:round(0.8*length(rand_num))),:); % 80
y_train = y(rand_num(1:round(0.8*length(rand_num))),:); % target

X_test = X(rand_num(round(0.8*length(rand_num))+1:end),:); % 20 ... 81:end-to-100 of samples
y_test = y(rand_num(round(0.8*length(rand_num))+1:end),:); % target
%% CV partition

c = cvpartition(y_train,'k',5);
%% feature selection show only 2 features out of features.size

opts = statset('display','iter');
classf = @(train_data, train_labels, test_data, test_labels)...
    sum(predict(fitcsvm(train_data, train_labels,'KernelFunction','rbf'), test_data) ~= test_labels);

[fs, history] = sequentialfs(classf, X_train, y_train, 'cv', c, 'options', opts,'nfeatures',2);
%% Best seperation hyperparameter MSE - weight(x) kernelScale - bias(y) BoxConstraint
% https://www.mathworks.com/help/stats/support-vector-machines-for-binary-classification.html

X_train_w_best_feature = X_train(:,fs);

Md1 = fitcsvm(X_train_w_best_feature,y_train,'KernelFunction','rbf','OptimizeHyperparameters','auto',...
      'HyperparameterOptimizationOptions',struct('AcquisitionFunctionName',...
      'expected-improvement-plus','ShowPlots',true)); % Bayes' Optimization use.


%% Final test with test set
X_test_w_best_feature = X_test(:,fs);
test_accuracy_for_iter = sum((predict(Md1,X_test_w_best_feature) == y_test))/length(y_test)*569; %100

%% hyperplane validation draw output

figure;
hgscatter = gscatter(X_train_w_best_feature(:,1),X_train_w_best_feature(:,2),y_train); % features values X_train_w_best_feature 1 col and col 2 compare
hold on;
h_sv=plot(Md1.SupportVectors(:,1),Md1.SupportVectors(:,2),'ko','markersize',8);
gscatter_malignant_group = hgscatter(2);
gscatter_malignant_group.Color = 'r';
X_label_matrix='';
X_flag_exit = 0;
Y_label_matrix='';
Y_flag_exit = 0;
searchMappingColumns0=0;
searchMappingRowsY0=0;
searchMappingColumns1=0;
% map data from X_train_w_best_feature to textFeatures
% X_train_w_best_feature{1}(1); only if there is cell like json array
% X_train_w_best_feature{2}(1);
for searchMappingColumns=1:30
    for searchMappingRowsY=1:569
        %disp(X_train_w_best_feature(1:1,1:1));
        %disp(X_train_w_best_feature(1:1,2:2));
        if (x(searchMappingRowsY:searchMappingRowsY,searchMappingColumns:searchMappingColumns) == X_train_w_best_feature(1:1,1:1) && Y_flag_exit == 0) % row from 1 to 1 and column from 1 to 1
            Y_label_matrix = textFeatures(searchMappingColumns);
            searchMappingColumns0=searchMappingColumns;
            searchMappingRowsY0 = searchMappingRowsY;
            Y_flag_exit = 1;
            %disp(Y_label_matrix);
            %break;
        end
    end
    for searchMappingRowsX=1:569
        %disp(X_train_w_best_feature(1:2));
        if (x(searchMappingRowsX:searchMappingRowsX,searchMappingColumns:searchMappingColumns) == X_train_w_best_feature(1:1,2:2) && X_flag_exit == 0) % row from 1 to 1 and column from 2 to 2
            % do not label different response feature but same value resulting in same axes name, after first iteration of for Y
            % we want to search same row record with same column feature of same patient
            % or to make sure that a record has a number but this number
            % presents multilpe times inside the data set we do a compare of not same column to
            % check again but with any of the other columns left to check
            % (e.g., same number in multiple columns but second
            % feature/column appears in another column not same record, different row yet
            % different record)
            if (searchMappingColumns0 ~= searchMappingColumns || searchMappingRowsX == searchMappingRowsY0)%searchMappingColumns0 ~= searchMappingColumns && searchMappingRowsX == searchMappingRowsY0
                X_label_matrix = textFeatures(searchMappingColumns);
                X_flag_exit = 1;
                %disp(X_label_matrix);
            %else
                %continue;
            end
        end
    end
    if (Y_flag_exit == 1 && X_flag_exit == 1)
        break;
    end
end
gscatter_malignant_group.Parent.XLabel.String = X_label_matrix;
gscatter_malignant_group.Parent.YLabel.String = Y_label_matrix;
gscatter_benign_group = hgscatter(1);
gscatter_benign_group.Color = 'b';

% test set of data put them one by one.

gscatter(X_test_w_best_feature(:,1),X_test_w_best_feature(:,2),y_test,'rb','xx')

% decision plane
XLIMs = get(gca,'xlim');
YLIMs = get(gca,'ylim');
[xi,yi] = meshgrid([XLIMs(1):0.01:XLIMs(2)],[YLIMs(1):0.01:YLIMs(2)]);
dd = [xi(:), yi(:)];
pred_mesh = predict(Md1, dd);
redcolor = [1, 0.8, 0.8];
bluecolor = [0.8, 0.8, 1];
pos = find(pred_mesh == 1);
h1 = plot(dd(pos,1), dd(pos,2),'s','color',redcolor,'Markersize',5,'MarkerEdgeColor',redcolor,'MarkerFaceColor',redcolor);
pos = find(pred_mesh == 2);
h2 = plot(dd(pos,1), dd(pos,2),'s','color',bluecolor,'Markersize',5,'MarkerEdgeColor',bluecolor,'MarkerFaceColor',bluecolor);
uistack(h1,'bottom');
uistack(h2,'bottom');
legend([hgscatter;h_sv],{'Benign','Malignant','support vectors'})
hold off