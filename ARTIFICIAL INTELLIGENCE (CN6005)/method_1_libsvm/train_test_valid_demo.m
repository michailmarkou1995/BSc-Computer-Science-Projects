% Breast Cancer classification using LIBSVM
% add libsvm HOME -> Set Path, add with subfolders choose folder "libsvm-3.25"

clear; clc; close all; % clear workspace variables loaded and console
load('wdbc.mat'); % load formated data set from file to workspace
P = size(x,2);  % number of standards x is p and inspect only columns and not rows (2) e.g., doc size

for iterations = 1:100 % each iteration (press keyboard while on pop-up window in-focus) gives a better result
    % Cross-validation
    [trainidx, testidx] = crossvalind('HoldOut', P, 0.2); % keep 20% of Data set Out
    % train set:
    xtrain = x(:,trainidx);
    ttrain = t(trainidx);
    % test set:
    xtest = x(:,testidx);
    ttest = t(testidx);
    % number of standards train
    Ptrain = sum(trainidx);
    % number of standards test
    Ptest = sum(testidx);
    
    % Train SVM model
    % kernel = RBF, gamma (influences generalization of model) = 0.0005, C = 100
    model = svmtrain(ttrain', xtrain', '-t 2 -g 0.0005 -c 100'); %svmtrain of libsvm (>> which trainsvm)
    fprintf('** Training prediction model:\n');
    predict_train = svmpredict(ttrain', xtrain', model);
    
    % Test model on the test set
    fprintf('** Testing prediction model:\n');
    predict_test = svmpredict(ttest', xtest', model); % predict on test data on trainned model
    
    % 1 figure with 2 subplots (up train-data, down test-data)
    % blue train/test model and red prediction targets
    % red dots must be on inside of blue for a good train model function prediction
    figure(1)
    subplot(2,1,1);
    plot(1:Ptrain, ttrain, 'bo', 1:Ptrain, predict_train, 'r.');
    subplot(2,1,2);
    plot(1:Ptest, ttest, 'bo', ...
        1:Ptest, predict_test, 'r.');
    pause;
end % ^C to exit the loop in terminal/cmd/cli